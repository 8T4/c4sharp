namespace C4Sharp.Models.Plantuml.Constants;

public record Shape
{
    /// <summary>
    /// This call returns the name of the rounded box shape and can be used as ?shape argument.
    /// </summary>
    public static Shape RoundedBoxShape => new() { Value = "RoundedBoxShape()" };
    /// <summary>
    /// This call returns the name of the eight sided shape and can be used as ?shape argument.
    /// </summary>
    public static Shape EightSidedShape => new() { Value = "EightSidedShape()" };

    public string Value { get; private init; } = string.Empty;
}
