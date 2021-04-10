using System;
using System.Collections.Generic;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models;
using C4Sharp.Models.Diagrams;
using C4Sharp.Models.SVG;

namespace C4Sharp.Graphic
{
    internal class Frame
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Padding { get; set; }
        public string Template { get; private set; }

        public string Content => ApplyReplacements();
        
        public Dictionary<string, Shape> Shapes { get; set; }

        public Frame(Diagram diagram, string template)
        {
            Padding = 50;
            Width = 1024;
            FillCanvas(diagram.Structures);
            Template = ResourceMethods.GetResource(template);
        }

        public override string ToString()
        {
            return ApplyReplacements();
        }

        private void FillCanvas(IEnumerable<Structure> structures)
        {
            Shapes = new Dictionary<string, Shape>();

            var (top, left) = (Padding, Padding);

            foreach (var structure in structures)
            {
                var svg = structure.ToSvg();
                svg.Move(top, left);

                Shapes[structure.Alias] = svg;

                top += svg.Height + (svg.Height/2);
            }

            Height = top + Padding;
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