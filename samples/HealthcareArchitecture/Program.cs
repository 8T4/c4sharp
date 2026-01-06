using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Plantuml;
using C4Sharp.Diagrams.Themes;
using C4Sharp.Diagrams.Drawio;
using HealthcareArchitecture.Diagrams;
using HealthcareArchitecture.Converters;
using System.Diagnostics;

Console.WriteLine("Building Healthcare Solution Architecture C4 diagrams...");

var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "../../generated-diagrams/healthcare");
Directory.CreateDirectory(outputPath);

// Load config for JSON-driven approach
var config = JsonToC4Converter.LoadConfig("config.json");

var diagrams = new DiagramBuilder[]
{
    new HealthcareContextDiagram(config),
    new HealthcareContainerDiagram(config),
    new HealthcareComponentDiagram(config)
};

var theme = new ParadisoTheme();

// Generate PUML files
Console.WriteLine("Generating PUML files...");
foreach (var diagramBuilder in diagrams)
{
    try
    {
        Console.WriteLine($"Generating {diagramBuilder.GetType().Name}...");
        
        var diagram = diagramBuilder.Build(theme);
        var plantuml = diagram.ToPumlString();
        
        var fileName = $"{diagram.Title.Replace(" ", "-").Replace(":", "").ToLower()}-{diagram.Type.ToString().ToLower()}.puml";
        var filePath = Path.Combine(outputPath, fileName);
        
        File.WriteAllText(filePath, plantuml);
        Console.WriteLine($"Generated: {fileName}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error generating {diagramBuilder.GetType().Name}: {ex.Message}");
    }
}

// Generate SVG files using PlantUML
Console.WriteLine("\nGenerating SVG files from PUML...");
try
{
    var plantumlJarPath = Path.Combine(Directory.GetCurrentDirectory(), "plantuml.jar");
    if (File.Exists(plantumlJarPath))
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "java",
            Arguments = $"-jar \"{plantumlJarPath}\" -tsvg \"{outputPath}\\*.puml\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        if (process != null)
        {
            process.WaitForExit();
            var output = process.StandardOutput.ReadToEnd();
            var error = process.StandardError.ReadToEnd();
            
            if (process.ExitCode == 0)
            {
                Console.WriteLine("SVG files generated successfully!");
            }
            else
            {
                Console.WriteLine($"PlantUML error: {error}");
            }
        }
    }
    else
    {
        Console.WriteLine("PlantUML JAR not found. SVG generation skipped.");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error generating SVG files: {ex.Message}");
}

// Generate Draw.io files
Console.WriteLine("\nGenerating Draw.io files...");
try
{
    var drawioPath = Path.Combine(outputPath, "drawio");
    Directory.CreateDirectory(drawioPath);
    
    new DrawioContext()
        .AddDiagrams(diagrams)
        .Export(drawioPath);
    
    Console.WriteLine("Draw.io files generated successfully!");
}
catch (Exception ex)
{
    Console.WriteLine($"Error generating Draw.io files: {ex.Message}");
}

Console.WriteLine($"\nHealthcare Solution Architecture diagrams generated in: {outputPath}");
Console.WriteLine("Generated formats: PUML, SVG (if Java available), Draw.io");
Console.WriteLine("\nDiagram types created:");
Console.WriteLine("- System Context Diagram (shows high-level systems and users)");
Console.WriteLine("- Container Diagram (shows system containers and their relationships)");
Console.WriteLine("- Component Diagram (shows internal components and interactions)");
