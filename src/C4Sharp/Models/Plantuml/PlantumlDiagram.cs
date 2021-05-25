using System.IO;
using System.Text;
using C4Sharp.FileSystem;
using C4Sharp.Models.Diagrams;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// Parser Diagram to PlantUML
    /// </summary>
    public static class PlantumlDiagram
    {
        private const string StandardLibraryBaseUrl = "https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master";

        /// <summary>
        /// Create PUML content from Diagram
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="useUrlInclude"></param>
        /// <returns></returns>
        public static string ToPumlString(this Diagram diagram, bool useUrlInclude = false)
        {
            var path = GetPumlFilePath(diagram, useUrlInclude);
                 
            var stream = new StringBuilder();
            stream.AppendLine($"@startuml {diagram.Slug()}");
            stream.AppendLine($"!include {path}");
            stream.AppendLine();

            if (!string.IsNullOrWhiteSpace(diagram.Title))
            {
                stream.AppendLine($"title {diagram.Title}");
            }

            if (diagram.LayoutWithLegend && !diagram.ShowLegend)
            {
                stream.AppendLine("LAYOUT_WITH_LEGEND()");
            }

            if (diagram.LayoutAsSketch)
            {
                stream.AppendLine("LAYOUT_AS_SKETCH()");
            }
            
            stream.AppendLine($"{(diagram.FlowVisualization == DiagramLayout.TopDown ? "LAYOUT_TOP_DOWN()" : "LAYOUT_LEFT_RIGHT()")}");
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

            if (diagram.ShowLegend)
            {
                stream.AppendLine();
                stream.AppendLine("SHOW_LEGEND()");
            }

            stream.AppendLine("@enduml");
            return stream.ToString();
        }

        private static string GetPumlFilePath(this Diagram diagram, bool useUrlInclude)
        {
            var pumlFileName = $"{diagram.Name}.puml";
            
            return useUrlInclude 
                ? $"{StandardLibraryBaseUrl}/{pumlFileName}"
                : Path.Join(C4Directory.ResourcesFolderName, pumlFileName);            
        }
    }
}