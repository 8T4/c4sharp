using System.IO;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Plantuml;

namespace C4Sharp.Models.SVG
{
    /// <summary>
    /// Parser Diagram to SVG
    /// </summary>
    internal static class SvgDiagram
    {
        public static string ToSvg(this Diagram diagram)
        {
            var resource = ResourceMethods.GetResource("diagram.svg");

            var stream = new StringBuilder();

            var y = 0;
     
            foreach (var structure in diagram.Structures)
            {
                stream.AppendLine(structure.ToSvg());
            }

            // foreach (var relationship in diagram.Relationships)
            // {
            //     stream.AppendLine(relationship.ToPumlString());
            // }

            return resource.Replace("{body}", stream.ToString()) ;
        }        
    }    
}