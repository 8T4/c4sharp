using System.CommandLine.Parsing;
using System.Diagnostics;
using System.Reflection;
using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml.IO;
using C4Sharp.Tools.Commands.Arguments;

namespace C4Sharp.Tools.Commands;

public class BuildCommand : Command
{
    public BuildCommand() : base("build",
        "Execute runners into solution that implements 'IDiagramBuildRunner' Interface")
    {
        RegisterArguments();
        Handler = CommandHandler.Create<string>(Execute);
    }

    private void RegisterArguments()
    {
        AddArgument(SolutionPathArgument.Get("path"));
    }

    private async Task<int> Execute(string path)
    {
        try
        {
            if (GetSolutionPath(path, out var slnPath, out var workspace) is false) 
                return 1;
        
            RunDotnetBuild(slnPath);
            var runners = await StartAnalysis(workspace, slnPath);
            GenerateC4Diagrams(runners);
            return 0;
        }
        catch (Exception e)
        {
            ColorConsole.WriteLine($"C4SCLI - ERROR - COMMAND: ".Red(), "BUILD");
            ColorConsole.WriteLine($"C4SCLI - ERROR - ARGS...: ".Red(), $"path={path}");
            ColorConsole.WriteLine($"C4SCLI - ERROR - OPTIONS: ".Red(), "NULL");
            ColorConsole.WriteLine($"C4SCLI - ERROR - MESSAGE: ".Red(), e.Message);
            ColorConsole.WriteLine(e.Message.White());
            
            return 1;
        }
    }

    /// <summary>
    /// Get solution path
    /// </summary>
    /// <param name="path">argument</param>
    /// <param name="slnPath">solution path</param>
    /// <param name="workspace">MSBuildWorkspace</param>
    /// <returns></returns>
    private static bool GetSolutionPath(string path, out string slnPath, out MSBuildWorkspace workspace)
    {
        slnPath = path;
        workspace = CreateWorkspace();
        
        if (slnPath != ".") return true;
        var slnFound = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.sln").FirstOrDefault();

        if (string.IsNullOrWhiteSpace(slnFound))
        {
            ColorConsole.WriteLine($"No solution was found in current directory {slnPath}".Red());
            {
                return false;
            }
        }

        slnPath = slnFound;
        ColorConsole.WriteLine("Solution found: ".White(), Path.GetFileName(slnPath).Green());
        return true;
    }

    /// <summary>
    /// Create a new MSBuildWorkspace instance
    /// </summary>
    /// <returns>MSBuildWorkspace</returns>
    private static MSBuildWorkspace CreateWorkspace()
    {
        if (!MSBuildLocator.IsRegistered)
        {
            var instances = MSBuildLocator.QueryVisualStudioInstances().ToArray();
            MSBuildLocator.RegisterInstance(instances.OrderByDescending(x => x.Version).First());
        }

        var workspace = MSBuildWorkspace.Create();
        workspace.SkipUnrecognizedProjects = true;
        return workspace;
    }

    /// <summary>
    /// Run dotnet build \"{slnPath}\"
    /// </summary>
    /// <param name="slnPath">solution path</param>
    private static void RunDotnetBuild(string slnPath)
    {
        ColorConsole.WriteLine("Running dotnet build".White());
        var dotnetProcess = Process.Start(new ProcessStartInfo(@"dotnet", $"build \"{slnPath}\"")
        {
            RedirectStandardError = true,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        }); 
        dotnetProcess!.WaitForExitAsync();
        ColorConsole.WriteLine("dotnet build complete".White());
    }

    /// <summary>
    /// Start Analysis from solution to search IDiagramBuildRunners implementations
    /// </summary>
    /// <param name="workspace"></param>
    /// <param name="slnPath"></param>
    private static async Task<IEnumerable<IDiagramBuildRunner>> StartAnalysis(MSBuildWorkspace workspace, string slnPath)
    {
        ColorConsole.WriteLine("Starting analysis".White());

        var solution = await workspace.OpenSolutionAsync(slnPath);

        var result = new List<IDiagramBuildRunner>();
        foreach (var project in solution.Projects)
        {
            ColorConsole.WriteLine("Analyzing project: ".White(), project.Name.Green());
            
            if (project.OutputFilePath is null)
            {
                ColorConsole.WriteLine("Output file path of project: ".Red(), project.Name.White(), " not found".Red());
                continue;
            }

            var type = typeof(IDiagramBuildRunner);

            var runners = Assembly.LoadFrom(project.OutputFilePath).GetTypes()
                .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                .Select(r => (IDiagramBuildRunner)Activator.CreateInstance(r)!).ToArray();

            if (!runners.Any())
            {
                ColorConsole.WriteLine("'IDiagramBuildRunner' implementations NOT FOUND into the project ".Yellow(), project.Name);
                continue;
            }
                
            result.AddRange(runners);
        }

        ColorConsole.WriteLine("Solution analysis is completed".White());
        return result;
    }
    
    /// <summary>
    /// Generate Diagrams
    /// </summary>
    /// <param name="runners"></param>
    private static void GenerateC4Diagrams(IEnumerable<IDiagramBuildRunner> runners)
    {
        ColorConsole.WriteLine("Generating C4 diagram".White());

        new PlantumlSession()
            .UseDiagramImageBuilder()
            .UseDiagramSvgImageBuilder()
            .Export(Path.Combine(Environment.CurrentDirectory, "c4"), runners.Select(r => r.Build()));

        ColorConsole.WriteLine();
        ColorConsole.WriteLine("C4 diagram PNG files".Green());
        var pngs = Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "c4"), "*.png");
        foreach (var png in pngs)
        {
            ColorConsole.WriteLine("C4 diagram generated: ".White(), $"file:///{png.Replace('\\', '/')}".Green());
        }
        
        ColorConsole.WriteLine();
        ColorConsole.WriteLine("C4 diagram SVG files".Green());
        var svgs = Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "c4"), "*.svg");
        foreach (var svg in svgs)
        {
            ColorConsole.WriteLine("C4 diagram generated: ".White(), $"file:///{svg.Replace('\\', '/')}".Green());
        }        
        
        ColorConsole.WriteLine();
        ColorConsole.WriteLine("C4 diagram PUML files".Green());
        var pumls = Directory.EnumerateFiles(Path.Combine(Environment.CurrentDirectory, "c4"), "*.puml");
        foreach (var puml in pumls)
        {
            ColorConsole.WriteLine("C4 diagram generated: ".White(), $"file:///{puml.Replace('\\', '/')}".Green());
        }
    }
}