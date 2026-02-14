using System;
using System.IO;
using System.Text.Json;
using System.Linq;
using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Plantuml;
using C4Sharp.Diagrams.Drawio;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var diagramsRoot = Path.Combine(currentDirectory, "diagrams");
            var outputDirectory = Path.Combine(currentDirectory, "generated-diagrams");

            if (!Directory.Exists(diagramsRoot))
            {
                Console.WriteLine($"Diagrams directory not found at: {diagramsRoot}");
                return;
            }

            var configFiles = Directory.GetFiles(diagramsRoot, "config.json", SearchOption.AllDirectories);

            foreach (var configFile in configFiles)
            {
                Console.WriteLine($"Processing: {configFile}");
                var jsonContent = File.ReadAllText(configFile);
                var config = JsonSerializer.Deserialize<DiagramConfig>(jsonContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                var structures = config.Structures.Select(CreateStructure).ToArray();

                var relationships = config.Relationships.Select(r => 
                {
                    var fromStructure = structures.First(s => s.Alias == r.From);
                    var toStructure = structures.First(s => s.Alias == r.To);
                    return fromStructure > toStructure | r.Label;
                }).ToArray();

                ConfigurableDiagram.CurrentTitle = config.Title;
                ConfigurableDiagram.CurrentSlug = config.Slug;
                ConfigurableDiagram.CurrentStructures = structures;
                ConfigurableDiagram.CurrentRelationships = relationships;

                var diagramBuilder = new ConfigurableDiagram();

                var plantumlPath = Path.Combine(outputDirectory, "plantuml");
                var drawioPath = Path.Combine(outputDirectory, "drawio");

                Directory.CreateDirectory(plantumlPath);
                Directory.CreateDirectory(drawioPath);

                try
                {
                    Console.WriteLine($"Exporting PlantUML diagrams to: {plantumlPath}");
                    new PlantumlContext()
                        .UseDiagramImageBuilder()        // Enable PNG generation
                        .UseDiagramSvgImageBuilder()     // Enable SVG generation
                        .UseDiagramMermaidBuilder()      // Enable Mermaid DSL generation
                        .Export(plantumlPath, new[] { diagramBuilder });
                    
                    Console.WriteLine("PlantUML export completed successfully (PUML, PNG, SVG, Mermaid DSL).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during PlantUML export: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                }

                try
                {
                    Console.WriteLine($"Exporting Draw.io diagrams to: {drawioPath}");
                    new DrawioContext()
                        .AddDiagrams(new[] { diagramBuilder })
                        .UseSvgExport()
                        .UsePngExport()
                        .Export(drawioPath);
                    Console.WriteLine("Draw.io export completed successfully (XML, SVG, PNG).");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during Draw.io export: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                }
            }

            Console.WriteLine("Diagram generation complete!");
        }

        private static Structure CreateStructure(StructureConfig config)
        {
            return config.Type switch
            {
                "Person" => new Person(config.Alias, config.Label) { Description = config.Description },
                "SoftwareSystem" => new SoftwareSystem(config.Alias, config.Label) { Description = config.Description },
                _ => throw new ArgumentException($"Unknown structure type: {config.Type}")
            };
        }
    }
}
