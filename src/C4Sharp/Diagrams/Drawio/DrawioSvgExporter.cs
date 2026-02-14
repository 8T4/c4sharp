using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Diagrams.Drawio
{
    public static class DrawioSvgExporter
    {
        public static void ExportSvg(string path, Diagram diagram)
        {
            var fileName = !string.IsNullOrEmpty(diagram.Title) 
                ? diagram.Title.Replace(" ", "-").Replace(":", "").Replace(",", "").ToLowerInvariant()
                : "diagram";
            var filePath = Path.Combine(path, $"{fileName}.svg");

            var svgContent = GenerateSvg(diagram);
            File.WriteAllText(filePath, svgContent);
        }

        private static string GenerateSvg(Diagram diagram)
        {
            var structures = diagram.Structures.ToList();
            var relationships = diagram.Relationships.ToList();
            
            // Calculate canvas size based on number of elements with proper spacing
            var cols = Math.Max(1, (int)Math.Ceiling(Math.Sqrt(structures.Count)));
            var rows = (int)Math.Ceiling((double)structures.Count / cols);
            var canvasWidth = Math.Max(1000, cols * 200 + 200); // Extra padding
            var canvasHeight = Math.Max(800, rows * 120 + 200 + relationships.Count * 5); // Extra padding

            var svg = new StringBuilder();
            svg.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            svg.AppendLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{canvasWidth}\" height=\"{canvasHeight}\" viewBox=\"0 0 {canvasWidth} {canvasHeight}\">");
            
            // Add marker definitions for arrows
            svg.AppendLine("  <defs>");
            svg.AppendLine("    <marker id=\"arrowhead\" markerWidth=\"10\" markerHeight=\"7\" refX=\"9\" refY=\"3.5\" orient=\"auto\">");
            svg.AppendLine("      <polygon points=\"0 0, 10 3.5, 0 7\" fill=\"#333\" />");
            svg.AppendLine("    </marker>");
            svg.AppendLine("  </defs>");
            
            // Add title
            if (!string.IsNullOrEmpty(diagram.Title))
            {
                svg.AppendLine($"  <text x=\"{canvasWidth / 2}\" y=\"30\" text-anchor=\"middle\" font-family=\"Arial, sans-serif\" font-size=\"18\" font-weight=\"bold\" fill=\"#333\">{diagram.Title}</text>");
            }

            // Calculate positions for structures
            var positions = CalculatePositions(structures.Count, canvasWidth, canvasHeight);

            // Draw structures
            for (var i = 0; i < structures.Count; i++)
            {
                var structure = structures[i];
                var position = positions[i];
                DrawStructure(svg, structure, position.x, position.y);
            }

            // Draw relationships
            foreach (var relationship in relationships)
            {
                var fromIndex = structures.FindIndex(s => s.Alias == relationship.From);
                var toIndex = structures.FindIndex(s => s.Alias == relationship.To);
                
                if (fromIndex >= 0 && toIndex >= 0)
                {
                    var fromPos = positions[fromIndex];
                    var toPos = positions[toIndex];
                    DrawRelationship(svg, relationship, fromPos, toPos);
                }
            }

            svg.AppendLine("</svg>");
            return svg.ToString();
        }

        private static void DrawStructure(StringBuilder svg, Structure structure, int x, int y)
        {
            var width = 160;
            var height = 80;
            var isPerson = structure is Person;
            
            // Choose colors based on structure type
            var fillColor = isPerson ? "#999999" : "#1168BD";
            var strokeColor = isPerson ? "#666666" : "#0E559C";
            var textColor = "#ffffff";

            // Draw rectangle
            svg.AppendLine($"  <rect x=\"{x}\" y=\"{y}\" width=\"{width}\" height=\"{height}\" " +
                          $"fill=\"{fillColor}\" stroke=\"{strokeColor}\" stroke-width=\"2\" rx=\"5\" ry=\"5\"/>");

            // Draw label
            var labelY = y + height / 2 - 5;
            svg.AppendLine($"  <text x=\"{x + width / 2}\" y=\"{labelY}\" text-anchor=\"middle\" " +
                          $"font-family=\"Arial, sans-serif\" font-size=\"12\" font-weight=\"bold\" fill=\"{textColor}\">{structure.Label}</text>");

            // Draw description if available
            if (!string.IsNullOrEmpty(structure.Description) && structure.Description.Length < 50)
            {
                var descY = labelY + 15;
                svg.AppendLine($"  <text x=\"{x + width / 2}\" y=\"{descY}\" text-anchor=\"middle\" " +
                              $"font-family=\"Arial, sans-serif\" font-size=\"10\" fill=\"{textColor}\">{structure.Description}</text>");
            }
        }

        private static void DrawRelationship(StringBuilder svg, Relationship relationship, (int x, int y) fromPos, (int x, int y) toPos)
        {
            var fromX = fromPos.x + 80; // Center of from structure
            var fromY = fromPos.y + 40;
            var toX = toPos.x + 80; // Center of to structure
            var toY = toPos.y + 40;

            // Draw arrow line
            svg.AppendLine($"  <line x1=\"{fromX}\" y1=\"{fromY}\" x2=\"{toX}\" y2=\"{toY}\" " +
                          "stroke=\"#333\" stroke-width=\"2\" marker-end=\"url(#arrowhead)\"/>");

            // Draw label at midpoint
            var midX = (fromX + toX) / 2;
            var midY = (fromY + toY) / 2;
            
            if (!string.IsNullOrEmpty(relationship.Label))
            {
                // Background rectangle for label
                var labelWidth = relationship.Label.Length * 8;
                svg.AppendLine($"  <rect x=\"{midX - labelWidth / 2}\" y=\"{midY - 10}\" width=\"{labelWidth}\" height=\"20\" " +
                              "fill=\"white\" stroke=\"#ccc\" stroke-width=\"1\" rx=\"3\" ry=\"3\"/>");
                
                svg.AppendLine($"  <text x=\"{midX}\" y=\"{midY + 5}\" text-anchor=\"middle\" " +
                              "font-family=\"Arial, sans-serif\" font-size=\"10\" fill=\"#333\">{relationship.Label}</text>");
            }
        }

        private static (int x, int y)[] CalculatePositions(int count, int canvasWidth, int canvasHeight)
        {
            var positions = new (int x, int y)[count];
            var cols = Math.Max(1, (int)Math.Ceiling(Math.Sqrt(count)));
            var rows = (int)Math.Ceiling((double)count / cols);
            
            var spacingX = (canvasWidth - 200) / Math.Max(1, cols - 1);
            var spacingY = (canvasHeight - 200) / Math.Max(1, rows - 1);
            
            for (var i = 0; i < count; i++)
            {
                var row = i / cols;
                var col = i % cols;
                
                var x = 100 + col * spacingX;
                var y = 80 + row * spacingY;
                
                positions[i] = (x, y);
            }

            return positions;
        }

        static DrawioSvgExporter()
        {
            // This will be called once to set up SVG definitions
        }
    }
}
