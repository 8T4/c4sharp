using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// PUML File Utils
    /// </summary>
    public static class PlantumlFile
    {
        public static void Save(Diagram diagram)
        {
            Directory.CreateDirectory("c4");
            var stream = GeneratePumlFileStream(diagram);

            File.WriteAllText($"c4/{diagram.Slug()}.puml", stream);
        }
        
        public static Task SaveAsync(Diagram diagram, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return Task.FromCanceled(cancellationToken);
            
            Save(diagram);

            return Task.CompletedTask;
        }        

        private static string GeneratePumlFileStream(Diagram diagram)
        {
            var path = Path.Join("..","bin", $"{diagram.PumlFileReference}.puml");
            
            var stream = new StringBuilder();
            stream.AppendLine($"@startuml {diagram.Slug()}");
            stream.AppendLine($"!include {path}");
            stream.AppendLine();
            stream.AppendLine($"{(diagram.LayoutWithLegend ? "LAYOUT_WITH_LEGEND()" : "")}");
            stream.AppendLine();

            foreach (var structure in diagram.Structures)
                stream.AppendLine(structure.ToString());

            stream.AppendLine();

            foreach (var relationship in diagram.Relationships)
                stream.AppendLine(relationship.ToString());

            stream.AppendLine($"@enduml");
            return stream.ToString();
        }

        public static void ExportToPng(Diagram diagram)
        {
            var dirPath = Directory.GetCurrentDirectory();
            var jarPath = Path.Join(dirPath, "bin", "plantuml.jar");
            var umlPath = Path.Join(dirPath, "c4", $"{diagram.Slug()}.puml");
            
            var jar = $"-jar {jarPath} -charset UTF-8";

            var info = new ProcessStartInfo
            {
                UseShellExecute = false,
                FileName = "java",
                Arguments = $"{jar} {umlPath}"
            };

            Process.Start(info)?.WaitForExit();
        }
    }
}