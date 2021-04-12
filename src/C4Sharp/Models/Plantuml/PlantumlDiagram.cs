using System.IO;
using System.Text;
using C4Sharp.FileSystem;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// Parser Diagram to PlantUML
    /// </summary>
    internal static class PlantumlDiagram
    {
        public static string ToPumlString(this Diagram diagram)
        {
            var path = Path.Join(C4Directory.ResourcesFolderName, $"{diagram.Name}.puml");
                 
            var stream = new StringBuilder();
            stream.AppendLine($"@startuml {diagram.Slug()}");
            stream.AppendLine($"!include {path}");
            stream.AppendLine();
            stream.AppendLine($"{(diagram.LayoutWithLegend ? "LAYOUT_WITH_LEGEND()" : "")}");
            stream.AppendLine();
     
            foreach (var structure in diagram.Structures)
            {
                stream.AppendLine(structure.ToPumlString());
            }

            stream.AppendLine();
     
            foreach (var relationship in diagram.Relationships)
            {
                stream.AppendLine(relationship.ToPumlString());
            }

            stream.AppendLine($"@enduml");
            return stream.ToString();
        }        
    }
}