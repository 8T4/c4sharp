using System;
using System.Collections.Generic;

namespace C4Sharp.Diagrams
{
    public class ElementStyle
    {
        public IDictionary<string, string> Items { get; }

        public ElementStyle()
        {
            Items = new Dictionary<string, string>();
        }
        
        public ElementStyle UpdateElementStyle(ElementName elementName, string bgColor, string fontColor="#ffffff", string borderColor="#00000000", bool shadowing = false, Shape? shape = null)
        {
            if (elementName is null)
                throw new ArgumentNullException(nameof(elementName), $"{nameof(elementName)} is required");
            
            var value = shape is null
                ? $"UpdateElementStyle(\"{elementName.Name}\", $bgColor={bgColor}, $fontColor={fontColor}, $borderColor={borderColor}, $shadowing=\"{shadowing}\")"
                : $"UpdateElementStyle(\"{elementName.Name}\", $bgColor={bgColor}, $fontColor={fontColor}, $borderColor={borderColor}, $shadowing=\"{shadowing}\", $shape={shape.Value})";

            Items[elementName.Name] = value;
            return this;
        }        
    }
}