using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Diagrams.Plantuml.Constants;

namespace C4Sharp.Diagrams.Plantuml.Style;

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
    /// <param name="borderStyle"></param>
    /// <param name="borderThickness"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public ElementStyle UpdateElementStyle(ElementName elementName, string? bgColor = null, string? fontColor = null,
        string? borderColor = null, bool shadowing = false, Shape? shape = null, BorderStyle? borderStyle = null,
        int? borderThickness = null)
    {
        if (elementName is null)
            throw new ArgumentNullException(nameof(elementName), $"{nameof(elementName)} is required");

        var styles = new List<string>();
        if (fontColor is not null)
            styles.Add($"$bgColor={bgColor}");

        if (bgColor is not null)
            styles.Add($"$fontColor={fontColor}");

        if (borderColor is not null)
            styles.Add($"$borderColor={borderColor}");

        styles.Add($"$shadowing=\"{shadowing.ToString().ToLower()}\"");

        if (shape is not null)
            styles.Add($"$shape={shape.Value}");

        if (borderStyle is not null)
            styles.Add($"borderStyle={borderStyle.Value}");

        if (borderThickness is not null)
            styles.Add($"$borderThickness={borderThickness}");

        Items[elementName.Name] = $"UpdateElementStyle(\"{elementName.Name}\", {string.Join(",", styles)})";
        return this;
    }

    public ElementStyle UpdateBoundaryStyle(ElementName elementName, string? bgColor = null, string? fontColor = null,
        string? borderColor = null, bool shadowing = false, Shape? shape = null, BorderStyle? borderStyle = null,
        int? borderThickness = null)
    {
        if (elementName is null)
            throw new ArgumentNullException(nameof(elementName), $"{nameof(elementName)} is required");

        var styles = new List<string>();
        styles.Add($"$elementName={elementName.Name}");

        if (fontColor is not null)
            styles.Add($"$bgColor={bgColor}");

        if (bgColor is not null)
            styles.Add($"$fontColor={fontColor}");

        if (borderColor is not null)
            styles.Add($"$borderColor={borderColor}");

        styles.Add($"$shadowing=\"{shadowing.ToString().ToLower()}\"");

        if (shape is not null)
            styles.Add($"$shape={shape.Value}");

        if (borderStyle is not null)
            styles.Add($"borderStyle={borderStyle.Value}");

        if (borderThickness is not null)
            styles.Add($"$borderThickness={borderThickness}");

        Items[elementName.Name] = $"UpdateBoundaryStyle({string.Join(",", styles)})";
        return this;
    }
}