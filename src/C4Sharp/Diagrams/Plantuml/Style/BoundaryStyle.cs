using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Diagrams.Plantuml.Constants;

namespace C4Sharp.Diagrams.Plantuml.Style;

public class BoundaryStyle : IBoundaryStyle
{
    public IDictionary<string, string> Items { get; } = new Dictionary<string, string>();

    /// <summary>
    /// Set the style of the boundary of the element (component, ...) and creates no additional legend entry.
    /// </summary>
    /// <param name="elementName"></param>
    /// <param name="bgColor"></param>
    /// <param name="fontColor"></param>
    /// <param name="borderColor"></param>
    /// <param name="shadowing"></param>
    /// <param name="shape"></param>
    /// <param name="borderStyle"></param>
    /// <param name="borderThickness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public BoundaryStyle UpdateBoundaryStyle(ElementName elementName, string? bgColor = null, string? fontColor = null,
        string? borderColor = null, bool shadowing = false, Shape? shape = null, BorderStyle? borderStyle = null,
        int? borderThickness = null)
    {
        string?[] styles =
        [
            $"$elementName={elementName.Name}",
            fontColor is not null ? $"$fontColor={fontColor}" : null,
            bgColor is not null ? $"$bgColor={bgColor}" : null,
            borderColor is not null ? $"$borderColor={borderColor}" : null,
            $"$shadowing=\"{shadowing.ToString().ToLower()}\"",
            shape is not null ? $"$shape={shape.Value}" : null,
            borderStyle is not null ? $"$borderStyle={borderStyle.Value}" : null,
            borderThickness is not null ? $"$borderThickness={borderThickness}" : null
        ];
        
        var styleString = string.Join(",", styles.Where(x => x is not null));

        Items[elementName.Name] = $"UpdateBoundaryStyle({styleString})";
        return this;
    }
}