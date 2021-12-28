namespace C4Sharp.Models.Plantuml.Constants;

public record LineStyle
{
    /// <summary>
    /// This call returns the name of the dashed line and can be used as ?lineStyle argument.
    /// </summary>
    public static LineStyle DashedLine => new() { Value = "DashedLine()" };
    /// <summary>
    /// This call returns the name of the dotted line and can be used as ?lineStyle argument.
    /// </summary>
    public static LineStyle DottedLine => new() { Value = "DottedLine()" };
    /// <summary>
    /// This call returns the name of the bold line and can be used as ?lineStyle argument.
    /// </summary>
    public static LineStyle BoldLine => new() { Value = "BoldLine()" };

    public string Value { get; private init; } = string.Empty;
}
