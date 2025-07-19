namespace C4Sharp.Diagrams.Plantuml.Constants;

public readonly record struct BorderStyle(string Value)
{
    /// <summary>
    /// This call returns the name of the dashed line and can be used as ?lineStyle argument.
    /// </summary>
    public static BorderStyle SolidLine => new("SolidLine()");

    /// <summary>
    /// This call returns the name of the dotted line and can be used as ?lineStyle argument.
    /// </summary>
    public static BorderStyle DashedLine => new("DashedLine()");

    /// <summary>
    /// This call returns the name of the bold line and can be used as ?lineStyle argument.
    /// </summary>
    public static BorderStyle DottedLine => new("DottedLine()");

    /// <summary>
    /// This call returns the name of the bold line and can be used as ?lineStyle argument.
    /// </summary>
    public static BorderStyle BoldLine => new("BoldLine()");
}