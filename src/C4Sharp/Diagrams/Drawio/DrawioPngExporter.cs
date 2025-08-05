using System;
using System.IO;
using System.Linq;
using System.Text;

namespace C4Sharp.Diagrams.Drawio
{
    public static class DrawioPngExporter
    {
        public static void ExportPng(string path, Diagram diagram)
        {
            var fileName = !string.IsNullOrEmpty(diagram.Title) 
                ? diagram.Title.Replace(" ", "-").Replace(":", "").Replace(",", "").ToLowerInvariant()
                : "diagram";
            var pngFilePath = Path.Combine(path, $"{fileName}.png");
            var svgFilePath = Path.Combine(path, $"{fileName}.svg");

            try
            {
                // For now, we'll create a placeholder PNG file with instructions
                // In a production environment, you would use a library like SkiaSharp, 
                // ImageSharp, or call external tools like Inkscape or Chrome headless
                var placeholderContent = GeneratePngPlaceholder(diagram);
                File.WriteAllText(pngFilePath, placeholderContent);
                
                Console.WriteLine($"PNG export placeholder created: {pngFilePath}");
                Console.WriteLine("To generate actual PNG files, consider integrating:");
                Console.WriteLine("- SkiaSharp for cross-platform SVG to PNG conversion");
                Console.WriteLine("- System.Drawing (Windows only)");
                Console.WriteLine("- External tools like Inkscape or Chrome headless");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating PNG for {fileName}: {ex.Message}");
            }
        }

        // SVG generation is handled by DrawioSvgExporter to avoid conflicts

        private static string GeneratePngPlaceholder(Diagram diagram)
        {
            var placeholder = new StringBuilder();
            placeholder.AppendLine("PNG Export Placeholder");
            placeholder.AppendLine("======================");
            placeholder.AppendLine();
            placeholder.AppendLine($"Diagram: {diagram.Title}");
            placeholder.AppendLine($"Structures: {diagram.Structures.Count()}");
            placeholder.AppendLine($"Relationships: {diagram.Relationships.Count()}");
            placeholder.AppendLine();
            placeholder.AppendLine("To implement actual PNG generation, you can:");
            placeholder.AppendLine();
            placeholder.AppendLine("1. Install SkiaSharp NuGet package:");
            placeholder.AppendLine("   dotnet add package SkiaSharp");
            placeholder.AppendLine("   dotnet add package SkiaSharp.Svg");
            placeholder.AppendLine();
            placeholder.AppendLine("2. Use System.Drawing (Windows only):");
            placeholder.AppendLine("   dotnet add package System.Drawing.Common");
            placeholder.AppendLine();
            placeholder.AppendLine("3. Use external tools:");
            placeholder.AppendLine("   - Inkscape: inkscape input.svg --export-png=output.png");
            placeholder.AppendLine("   - Chrome: chrome --headless --screenshot=output.png input.html");
            placeholder.AppendLine();
            placeholder.AppendLine("Note: SVG files are generated separately by DrawioSvgExporter");
            placeholder.AppendLine();
            placeholder.AppendLine("Generated at: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            
            return placeholder.ToString();
        }
    }
}
