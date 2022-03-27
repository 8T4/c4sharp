using System.Diagnostics;
using System.Text;
using C4Sharp.Diagrams;
using C4Sharp.Extensions;
using C4Sharp.FileSystem;
using C4Sharp.Models.Plantuml.Extensions;

namespace C4Sharp.Models.Plantuml.IO;

public partial class PlantumlContext : IDisposable
{
    private bool StandardLibraryBaseUrl { get; set; }
    private bool GenerateDiagramImages { get; set; }
    private bool GenerateHtmlPages { get; set; }
    private bool GenerateDiagramSvgImages { get; set; }
    private string? PlantumlJarPath { get; set; }
    private ProcessStartInfo ProcessInfo { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public PlantumlContext()
    {
        PlantumlJarPath = null;
        StandardLibraryBaseUrl = false;
        GenerateDiagramImages = false;
        GenerateDiagramSvgImages = false;
        GenerateHtmlPages = false;

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
    public PlantumlContext UseStandardLibraryBaseUrl()
    {
        StandardLibraryBaseUrl = true;
        return this;
    }

    /// <summary>
    /// The C4Sharp will generate *.puml files of your diagram.
    /// Also, you could save the *.png files using this method
    /// </summary>
    /// <returns></returns>
    public PlantumlContext UseDiagramImageBuilder()
    {
        GenerateDiagramImages = true;
        return this;
    }

    /// <summary>
    /// The C4Sharp will generate *.html page for your diagrams images.
    /// </summary>
    /// <returns></returns>
    public PlantumlContext UseHtmlPageBuilder()
    {
        GenerateHtmlPages = true;
        GenerateDiagramSvgImages = true;
        return this;
    }

    /// <summary>
    /// The C4Sharp will generate *.puml files of your diagram.
    /// Also, you could save the *.svg files using this method
    /// </summary>
    /// <returns></returns>
    public PlantumlContext UseDiagramSvgImageBuilder()
    {
        GenerateDiagramSvgImages = true;
        return this;
    }
    
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
    /// ReSharper disable once MemberCanBePrivate.Global
    public void Export(string path, IEnumerable<Diagram> diagrams)
    {
        var enumerable = diagrams as Diagram[] ?? diagrams.ToArray();
        
        foreach (var diagram in enumerable)
        {
            SavePumlFiles(diagram, path);
        }

        if (GenerateDiagramImages)
        {
            SaveDiagramFiles(path, "png");
        }

        if (GenerateDiagramSvgImages)
        {
            SaveDiagramFiles(path, "svg");
        }
        
        if (GenerateHtmlPages)
        {
            SaveHtmlFiles(path, enumerable);
        }        
    }    
}

/// <summary>
/// PUML, SVG, PNG file utils
/// </summary>
public partial class PlantumlContext
{
    /// <summary>
    /// Save puml file. It's creates path if non exists.
    /// </summary>
    /// <param name="diagram">C4 Diagram</param>
    /// <param name="path">Output path</param>
    private void SavePumlFiles(Diagram diagram, string path)
    {
        try
        {
            var filePath = Path.Combine(path, diagram.PumlFileName());
            Directory.CreateDirectory(path);
            File.WriteAllText(filePath, diagram.ToPumlString(StandardLibraryBaseUrl));
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not save puml file.", e);
        }
    }

    /// <summary>
    /// Execute plantuml.jar
    /// </summary>
    /// <param name="path">puml files path</param>
    /// <param name="generatedImageFormat">specifies the format of the generated images</param>
    /// <exception cref="PlantumlException"></exception>
    private void SaveDiagramFiles(string path, string generatedImageFormat)
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
/// HTML file utils
/// </summary>
public partial class PlantumlContext
{
    private static void SaveHtmlFiles(string path, IEnumerable<Diagram> diagrams)
    {
        try
        {
            Directory.CreateDirectory(path);
            PlantumlResources.LoadHtmlResources(path);
            var enumerable = diagrams as Diagram[] ?? diagrams.ToArray();
            
            var index = Path.Combine(path, "index.html");
            File.WriteAllText(index, enumerable.ToIndexHtmlPage());
            
            foreach (var diagram in enumerable)
            {
                var filePath = Path.Combine(path, diagram.HtmlPageName());
                File.WriteAllText(filePath, diagram.ToHtmlPage());                
            }
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not save html file.", e);
        }        
    }
}