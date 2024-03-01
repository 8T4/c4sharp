using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using C4Sharp.Diagrams.Plantuml;

namespace C4Sharp.Commons;

/// <summary>
/// Methods to manipulate resource
/// </summary>
[ExcludeFromCodeCoverage]
internal static class ResourceFile
{
    /// <summary>
    /// Get resource string content from resource file
    /// </summary>
    /// <param name="name">file name</param>
    /// <returns>resource content</returns>
    /// <exception cref="PlantumlException"></exception>
    public static string ReadString(string name)
    {
        try
        {
            using var stream = ReadStream(name);
            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            return reader.ReadToEnd();
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not get resource.", e);
        }
    }

    /// <summary>
    /// Get resource stream content by resource name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="PlantumlException"></exception>
    public static Stream? ReadStream(string name)
    {
        try
        {
            return Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"C4Sharp.Resources.{name}");
        }
        catch (Exception e)
        {
            throw new PlantumlException($"{nameof(PlantumlException)}: Could not get resource.", e);
        }
    }
}
