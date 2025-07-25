using System.Diagnostics;
using System.Text;
using C4Sharp.Commons.FileSystem;
using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Diagrams.Themes;

namespace C4Sharp.Diagrams.Plantuml;

public partial class PlantumlContext : IDisposable
{
    private bool GenerateDiagramImages { get; set; }
    private bool GenerateDiagramSvgImages { get; set; }
    private bool GenerateMermaidFiles { get; set; }
    private string? PlantumlJarPath { get; set; }
    private ProcessStartInfo ProcessInfo { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    public PlantumlContext()
    {
        PlantumlJarPath = null;
        GenerateDiagramImages = false;
        GenerateDiagramSvgImages = false;
        GenerateMermaidFiles = false;

        ProcessInfo = new ProcessStartInfo
        {
            FileName = "java",
            UseShellExecute = false,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
        };
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
    /// The C4Sharp will generate *.mermaid.md files of your diagram.
    /// </summary>
    /// <returns></returns>
    public PlantumlContext UseDiagramMermaidBuilder()
    {
        GenerateMermaidFiles = true;
        return this;
    }

    /// <summary>
    /// It creates a Puml file into the default directory "./c4"
    /// If the attribute of Session GenerateDiagramImages is true
    /// It generates png files of the diagram
    /// </summary>
    /// <param name="diagrams">C4 Diagrams</param>
    /// <param name="theme"></param>
    public void Export(IEnumerable<IDiagramBuilder> diagrams, IDiagramTheme? theme = null) => 
        InternalExport(diagrams.Select(d => d.Build(theme ?? new DefaultTheme())));

    /// <summary>
    /// It creates a Puml file into the default directory "./c4"
    /// If the attribute of Session GenerateDiagramImages is true
    /// It generates png files of the diagram
    /// </summary>
    /// <param name="diagrams">C4 Diagrams</param>
    [Obsolete("Use the method with IDiagramBuilder")]
    public void Export(IEnumerable<Diagram> diagrams) => 
        InternalExport(diagrams);

    /// <summary>
    /// It creates a Puml file into the default directory "./c4"
    /// If the attribute of Session GenerateDiagramImages is true
    /// It generates png files of the diagram
    /// </summary>
    /// <param name="diagrams">C4 Diagrams</param>
    private void InternalExport(IEnumerable<Diagram> diagrams)
    {
        var dirPath = Directory.GetCurrentDirectory();
        var path = Path.Join(dirPath, C4SharpDirectory.DirectoryName);
        InternalExport(path, diagrams);
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
    /// <param name="theme"></param>
    /// ReSharper disable once MemberCanBePrivate.Global
    public void Export(string path, IEnumerable<IDiagramBuilder> diagrams, IDiagramTheme? theme = null) => 
        InternalExport(path, diagrams.Select(d => d.Build(theme)));

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
    [Obsolete("Use the method with IDiagramBuilder")]
    public void Export(string path, IEnumerable<Diagram> diagrams) => InternalExport(path, diagrams);

    private void InternalExport(string path, IEnumerable<Diagram> diagrams)
    {
        var enumerable = diagrams as Diagram[] ?? diagrams.ToArray();
        
        foreach (var diagram in enumerable)
        {
            SavePumlFiles(diagram, path);
            SaveMermaidFiles(diagram, path);
        }

        if (GenerateDiagramImages) SaveDiagramFiles(path, "png");
        if (GenerateDiagramSvgImages) SaveDiagramFiles(path, "svg");
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
            File.WriteAllText(filePath, diagram.ToPumlString());
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not save puml file.", e);
        }
    }

    /// <summary>
    /// Save puml file. It's creates path if non exists.
    /// </summary>
    /// <param name="diagram">C4 Diagram</param>
    /// <param name="path">Output path</param>
    private void SaveMermaidFiles(Diagram diagram, string path)
    {
        if (!GenerateMermaidFiles || diagram.Type.Value == DiagramConstants.Deployment) return;

        try
        {
            var filePath = Path.Combine(path, diagram.MermaidFileName());
            Directory.CreateDirectory(path);
            File.WriteAllText(filePath, diagram.ToMermaidString());
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not save mermaid.md file.", e);
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
            PlantumlJarPath ??= PlantumlResources.LoadPlantumlJar();

            var directory = new DirectoryInfo(path).FullName;

            if (string.IsNullOrEmpty(directory))
            {
                throw new PlantumlException($"{nameof(PlantumlException)}: puml file not found.");
            }

            var results = new StringBuilder();

            var jar = CalculateJarCommand(generatedImageFormat, directory);

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

    private string CalculateJarCommand(string generatedImageFormat, string directory)
    {
        var imageFormatOutputArg = string.IsNullOrWhiteSpace(generatedImageFormat)
            ? string.Empty
            : $"-t{generatedImageFormat}";

        return
            $"-jar \"{PlantumlJarPath}\" {imageFormatOutputArg} -Playout=smetana -verbose -o \"{directory}\" -charset UTF-8";
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