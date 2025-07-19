using System.Diagnostics.CodeAnalysis;

namespace C4Sharp.Commons;

/// <summary>
/// Indentation methods
/// </summary>
[ExcludeFromCodeCoverage]
internal readonly record struct TabIndentation
{
    /// <summary>
    /// Default indentation.
    /// </summary>
    internal static int TabSize => 4;

    /// <summary>
    /// Ident string using default tab size.
    /// </summary>
    /// <returns>indented text</returns>
    internal static string Indent() =>
        string.Empty.PadLeft(TabSize);

    /// <summary>
    /// Ident string using tab size.
    /// </summary>
    /// <param name="size">tab size</param>
    /// <returns>indented text</returns>
    internal static string Indent(int size) =>
        string.Empty.PadLeft(size);
}
