using C4Sharp.Diagrams;
using C4Sharp.Diagrams.Plantuml;
using C4Sharp.Diagrams.Drawio;
using C4Sharp.Diagrams.Themes;

namespace C4Sharp.Diagrams
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generating C4 Healthcare Solution Diagrams...");
            Console.WriteLine("==============================================");

            // Define output paths
            var baseOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "generated-diagrams");
            var plantumlPath = Path.Combine(baseOutputPath, "c4-plantuml");
            var drawioPath = Path.Combine(baseOutputPath, "c4-drawio");

            // Ensure output directories exist
            Directory.CreateDirectory(plantumlPath);
            Directory.CreateDirectory(drawioPath);

            // Create C4 diagram instances
            var diagrams = new IDiagramBuilder[]
            {
                new HealthcareSystemContextDiagram(),
                new HealthcareContainerDiagram(),
                new HealthcareComponentDiagram()
            };

            try
            {
                // Export PlantUML diagrams with all formats
                Console.WriteLine($"Exporting PlantUML diagrams to: {plantumlPath}");
                new PlantumlContext()
                    .UseDiagramImageBuilder()        // Enable PNG generation
                    .UseDiagramSvgImageBuilder()     // Enable SVG generation
                    .UseDiagramMermaidBuilder()      // Enable Mermaid DSL generation
                    .Export(plantumlPath, diagrams, new DefaultTheme());
                
                Console.WriteLine("PlantUML export completed successfully (PUML, PNG, SVG, Mermaid DSL).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during PlantUML export: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            try
            {
                // Export Draw.io diagrams with all formats
                Console.WriteLine($"Exporting Draw.io diagrams to: {drawioPath}");
                new DrawioContext()
                    .AddDiagrams(diagrams)
                    .UseSvgExport()                  // Enable SVG generation
                    .UsePngExport()                  // Enable PNG placeholder generation
                    .Export(drawioPath);
                
                Console.WriteLine("Draw.io export completed successfully (XML, SVG, PNG).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during Draw.io export: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }

            Console.WriteLine();
            Console.WriteLine("C4 Diagram Generation Complete!");
            Console.WriteLine("================================");
            Console.WriteLine();
            Console.WriteLine("Generated diagrams:");
            Console.WriteLine($"- System Context Diagram: Shows healthcare system and external actors");
            Console.WriteLine($"- Container Diagram: Shows internal containers (web apps, APIs, databases)");
            Console.WriteLine($"- Component Diagram: Shows components within Patient Management Service");
            Console.WriteLine();
            Console.WriteLine("Output formats:");
            Console.WriteLine($"- PlantUML: .puml files, .mermaid.md files (+ PNG/SVG if PlantUML JAR available)");
            Console.WriteLine($"- Draw.io: .drawio XML files, .svg files, .png placeholder files");
            Console.WriteLine();
            Console.WriteLine($"PlantUML files: {plantumlPath}");
            Console.WriteLine($"Draw.io files: {drawioPath}");
        }
    }
}
