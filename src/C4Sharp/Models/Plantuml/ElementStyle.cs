using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml.Constants;

namespace C4Sharp.Models.Plantuml;

public class ElementStyle : IElementStyle
{
    public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

    /// <summary>
    /// This call updates the default style of the elements (component, ...) and creates no additional legend entry.
    /// </summary>
    /// <param name="elementName"></param>
    /// <param name="bgColor"></param>
    /// <param name="fontColor"></param>
    /// <param name="borderColor"></param>
    /// <param name="shadowing"></param>
    /// <param name="shape"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public ElementStyle UpdateElementStyle(ElementName elementName, string bgColor, string fontColor = "#ffffff", string borderColor = "#00000000", bool shadowing = false, Shape? shape = null)
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
