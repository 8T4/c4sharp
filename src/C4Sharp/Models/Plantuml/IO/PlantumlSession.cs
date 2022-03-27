using System.Diagnostics;
using System.Text;
using C4Sharp.Diagrams;
using C4Sharp.FileSystem;
using C4Sharp.Models.Plantuml.Extensions;

namespace C4Sharp.Models.Plantuml.IO;

/// <summary>
/// Session
/// </summary>
[Obsolete("This class is deprecated! Please, use PlantumlContext")]
public partial class PlantumlSession : IDisposable
{
    public bool StandardLibraryBaseUrl { get; private set; }
    public bool GenerateDiagramImages { get; private set; }
    public bool GenerateDiagramSvgImages { get; private set; }
    private string? PlantumlJarPath { get; set; }
    private ProcessStartInfo ProcessInfo { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public PlantumlSession()
    {
        PlantumlJarPath = null;
        ProcessInfo = new ProcessStartInfo
        {
            FileName = "java",
            UseShellExecute = false,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
        };
    }

    /// <summary>
    /// The C4Sharp has embedded the current version of C4-PluntUML.
    /// But, if you want to use the C4-PlantUML up-to-date version from their repo,
    /// use this method
    /// </summary>
    /// <returns>PlantumlSession instance</returns>
    public PlantumlSession UseStandardLibraryBaseUrl()
    {
        StandardLibraryBaseUrl = true;
        return this;
    }

    /// <summary>
    /// The C4Sharp will generate *.puml files of your diagram.
    /// Also, you could save the *.png files using this method
    /// </summary>
    /// <returns></returns>
    public PlantumlSession UseDiagramImageBuilder()
    {
        GenerateDiagramImages = true;
        return this;
    }

    /// <summary>
    /// The C4Sharp will generate *.puml files of your diagram.
    /// Also, you could save the *.svg files using this method
    /// </summary>
    /// <returns></returns>
    public PlantumlSession UseDiagramSvgImageBuilder()
    {
        GenerateDiagramSvgImages = true;
        return this;
    }

    /// <summary>
    /// Execute plantuml.jar
    /// </summary>
    /// <param name="path">puml files path</param>
    /// <param name="generatedImageFormat">specifies the format of the generated images</param>
    /// <exception cref="PlantumlException"></exception>
    public void Execute(string path, string generatedImageFormat)
    {
        try
        {
            PlantumlResources.LoadResources(path);
            PlantumlJarPath ??= PlantumlResources.LoadPlantumlJar();

            var directory = new DirectoryInfo(path).FullName;

            if (string.IsNullOrEmpty(directory))
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.");
            }

            var results = new StringBuilder();

            var jar = CalculateJarCommand(StandardLibraryBaseUrl, generatedImageFormat, directory);

            ProcessInfo.Arguments = $"{jar} \"{path}\"";
            ProcessInfo.RedirectStandardOutput = true;
            ProcessInfo.StandardOutputEncoding = Encoding.UTF8;

            var process = new Process { StartInfo = ProcessInfo };

            process.OutputDataReceived += (_, args) => results.AppendLine(args.Data);

            process.Start();
            process.WaitForExit();
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.", e);
        }
    }

    private string CalculateJarCommand(bool useStandardLibrary, string generatedImageFormat, string directory)
    {
        const string includeLocalFilesArg = "-DRELATIVE_INCLUDE=\".\"";

        var resourcesOriginArg = useStandardLibrary ? string.Empty : includeLocalFilesArg;
        var imageFormatOutputArg = string.IsNullOrWhiteSpace(generatedImageFormat)
            ? string.Empty
            : $"-t{generatedImageFormat}";

        return
            $"-jar \"{PlantumlJarPath}\" {resourcesOriginArg} {imageFormatOutputArg} -Playout=smetana -verbose -o \"{directory}\" -charset UTF-8";
    }

    /// <summary>
    /// Using the -pipe option, you can easily use PlantUML in your scripts.
    /// With this option, a diagram description is received through standard input and the PNG file is generated to standard output.
    /// No file is written on the local file system.
    /// </summary>
    /// <param name="input">puml content</param>
    /// <exception cref="PlantumlException"></exception>
    internal (string, Stream) GetStream(string input)
    {
        try
        {
            PlantumlJarPath ??= PlantumlResources.LoadPlantumlJar();

            var results = new StringBuilder();
            var fileName = Guid.NewGuid().ToString("N");
            var standardLibraryBaseUrlArgs = StandardLibraryBaseUrl ? string.Empty : "-DRELATIVE_INCLUDE=\".\"";
            var jar = $"-jar {PlantumlJarPath} {standardLibraryBaseUrlArgs} -Playout=smetana -verbose -charset UTF-8";

            ProcessInfo.Arguments = $"{jar} -pipe > {fileName}.png";
            ProcessInfo.RedirectStandardOutput = true;
            ProcessInfo.RedirectStandardInput = true;
            ProcessInfo.StandardOutputEncoding = Encoding.UTF8;

            var process = new Process { StartInfo = ProcessInfo };

            process.OutputDataReceived += (_, args) => results.AppendLine(args.Data);

            process.Start();
            process.StandardInput.Write(input);
            process.StandardInput.Flush();
            process.StandardInput.Close();
            process.BeginOutputReadLine();
            process.WaitForExit();

            var buffer = Encoding.UTF8.GetBytes(results.ToString());
            return (fileName, new MemoryStream(buffer));
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.", e);
        }
    }

    /// <summary>
    /// Clear Plantuml Resource
    /// </summary>
    public void Dispose()
    {
        try
        {
            if (PlantumlJarPath is not null)
            {
                File.Delete(PlantumlJarPath);
            }
        }
        catch
        {
            // ignored
        }
    }
}

/// <summary>
/// PUML File Utils
/// </summary>
public partial class PlantumlSession
{
    private static readonly object Lock = new();

    /// <summary>
    /// It creates a Puml file into the default directory "./c4"
    /// If the attribute of Session GenerateDiagramImages is true
    /// It generates png files of the diagram
    /// </summary>
    /// <param name="diagrams">C4 Diagrams</param>
    public void Export(IEnumerable<Diagram> diagrams)
    {
        var dirPath = Directory.GetCurrentDirectory();
        var path = Path.Join(dirPath, C4SharpDirectory.DirectoryName);
        Export(path, diagrams);
    }

    /// <summary>
    /// It creates a Puml file into the default directory "./c4"
    /// If the attribute of Session GenerateDiagramImages is true
    /// It generates png files of the diagram
    /// </summary>
    /// <param name="diagrams">C4 Diagrams</param>
    /// <param name="path">
    /// Full path of the directory
    /// <example>For windows.: C:\users\user\projects\</example>
    /// <example>For Unix.: users/user/projects/</example>
    /// </param>
    public void Export(string path, IEnumerable<Diagram> diagrams)
    {
        foreach (var diagram in diagrams)
        {
            Save(diagram, path);
        }

        if (GenerateDiagramImages)
        {
            Execute(path, "png");
        }

        if (GenerateDiagramSvgImages)
        {
            Execute(path, "svg");
        }
    }

    /// <summary>
    /// Save puml file. It's creates path if non exists.
    /// </summary>
    /// <param name="diagram">C4 Diagram</param>
    /// <param name="path">Output path</param>
    private void Save(Diagram diagram, string path)
    {
        try
        {
            lock (Lock)
            {
                var filePath = Path.Combine(path, $"{diagram.Slug()}.puml");
                Directory.CreateDirectory(path);
                File.WriteAllText(filePath, diagram.ToPumlString(StandardLibraryBaseUrl));
            }
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not save puml file.", e);
        }
    }
}