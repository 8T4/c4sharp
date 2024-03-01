namespace C4Sharp.Diagrams.Plantuml.Constants;

public class BorderStyle
{
    /// <summary>
    /// This call returns the name of the dashed line and can be used as ?lineStyle argument.
    /// </summary>
    public static BorderStyle SolidLine => new() { Value = "SolidLine()" };
    /// <summary>
    /// This call returns the name of the dotted line and can be used as ?lineStyle argument.
    /// </summary>
    public static BorderStyle DashedLine => new() { Value = "DashedLine()" };
    /// <summary>
    /// This call returns the name of the bold line and can be used as ?lineStyle argument.
    /// </summary>
    public static BorderStyle DottedLine => new() { Value = "DottedLine()" };
    public static BorderStyle BoldLine => new() { Value = "BoldLine()" };

    public string Value { get; private init; } = string.Empty;    
}