// See https://aka.ms/new-console-template for more information

using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Plantuml;
using C4Sharp.Diagrams.Themes;
using ModelDiagrams.Diagrams;

Console.WriteLine("Building C4 diagrams...");

var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "../../generated-diagrams");
Directory.CreateDirectory(outputPath);

var diagrams = new DiagramBuilder[]
{
    new ContextDiagramSample(),
    new ComponentDiagramSample(), 
    new ContainerDiagramSample(),
    new EnterpriseDiagramSample(),
    new SequenceDiagramSample(),
    new DeploymentDiagramSample(),
    new RemittanceAdviceContainerDiagram()
};

var theme = new ParadisoTheme();

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

Console.WriteLine($"\nC4 diagrams generated in: {outputPath}");
Console.WriteLine("PUML files created successfully!");
