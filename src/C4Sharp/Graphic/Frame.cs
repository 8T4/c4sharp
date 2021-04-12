using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.Relationships;
using C4Sharp.Models.SVG;

namespace C4Sharp.Graphic
{
    internal class Frame
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Template { get; private set; }
        public Dictionary<string, Shape> Shapes { get; set; }
        public string Content => ApplyReplacements();

        public Frame(Diagram diagram, string template)
        {
            Height = 0;
            Width = 0;
            Shapes = new Dictionary<string, Shape>();
            Template = ResourceMethods.GetResource(template);

            FillCanvas(diagram);
        }

        public override string ToString()
        {
            return ApplyReplacements();
        }

        private void FillCanvas(Diagram diagram)
        {
            var (top, left) = (0, 0);
            var (size, midd) = GetGrid(diagram);
            var grid = new Shape[size, size];
            
            
            foreach (var shape in Shapes)
            {
            }            
            
            foreach (var structure in diagram.Structures)
            {
                var svg = structure.ToSvg();

                left = svg.Width * midd;
                top = top == 0 ? svg.Marging.top : top;
                svg.Move(top, left);

                Shapes[structure.Alias] = svg;

                top += svg.TotalHeight;
                Height = top;
                Width = (svg.TotalWidth) * size;
            }

            foreach (var shape in Shapes)
            {
                var rell =
                    from r in diagram.Relationships
                    where r.From == shape.Value.Id &&
                          r.Position == Position.Left
                    select r;

                var relr =
                    from r in diagram.Relationships
                    where r.From == shape.Value.Id &&
                          (r.Position == Position.Left ||
                           r.Position == Position.Neighbor)
                    select r;
            }
        }

        private (int size, int midd) GetGrid(Diagram diagram)
        {
            var size = diagram.Structures.Length % 2 == 0
                ? diagram.Structures.Length + 1
                : diagram.Structures.Length;

            var midd = (int) Math.Ceiling((double) size / 2);

            return (size, midd);
        }

        private string ApplyReplacements()
        {
            var content = Template;
            var stream = new StringBuilder();

            foreach (var shape in Shapes)
            {
                stream.AppendLine(shape.Value.Content);
            }

            content = content
                .Replace("{body}", stream.ToString())
                .Replace("{height}", Height.ToString())
                .Replace("{width}", Width.ToString());

            return content;
        }
    }
}