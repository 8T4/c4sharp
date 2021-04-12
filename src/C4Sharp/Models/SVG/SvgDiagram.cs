using System.IO;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Graphic;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Plantuml;

namespace C4Sharp.Models.SVG
{
    /// <summary>
    /// Parser Diagram to SVG
    /// </summary>
    internal static class SvgDiagram
    {
        public static string ExportToSvg(this Diagram diagram)
        {
            var frame = new Frame(diagram, "template_diagram.svg");
            return frame.ToString();
        }        
    }    
}