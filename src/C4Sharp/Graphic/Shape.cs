using System.Collections.Generic;
using System.IO;
using C4Sharp.Extensions;

namespace C4Sharp.Graphic
{
    internal class Shape
    {
        public string Id { get; private set; }
        public string Background { get; private set; }
        public string Template { get; }
        public Dictionary<string, string> Replacements { get; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public int Left { get; private set; }
        public int Top { get; private set; }
        public int Marging { get; private set; }
        
        public string Content => ApplyReplacements();

        public Shape(string id, string templateName)
        {
            Background = string.Empty;
            Id = id;
            Left = 0;
            Top = 0;
            Height = 0;
            Width = 0;
            Marging = 0; 
            Replacements = new Dictionary<string, string>();
            Template = ResourceMethods.GetResource(templateName);
        }

        public Shape(string id, int width, int height, string templateName)
        {
            Id = id;
            Left = 0;
            Top = 0;
            Height = height;
            Width = width;
            Marging = 0; 
            Replacements = new Dictionary<string, string>();
            Template = ResourceMethods.GetResource(templateName);
        }

        public override string ToString()
        {
            return ApplyReplacements();
        }

        private string ApplyReplacements()
        {
            var content = Template;
            foreach (var (key, value) in Replacements)
            {
                content = content
                    .Replace(key, value);
            }
            
            content = content
                .Replace("{id}",Id)
                .Replace("{background}", Background)
                .Replace("{height}", Height.ToString())
                .Replace("{width}", Width.ToString())
                .Replace("{top}", Top.ToString())
                .Replace("{left}", Left.ToString());

            return content;
        }

        public Shape Move(int top, int left)
        {
            Top = top;
            Left = left;
            return this;
        }

        public Shape Fill(string color)
        {
            Background = color;
            return this;
        }
        
        public Shape Resize(int width, int height)
        {
            Width = width;
            Height = height;
            return this;
        }        
        
        public Shape Replace(string oldValue, string newValue)
        {
            Replacements[oldValue] = newValue;
            return this;
        }
    }
}