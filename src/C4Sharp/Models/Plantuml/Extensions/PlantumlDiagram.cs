using C4Sharp.Diagrams;
using C4Sharp.FileSystem;
using C4Sharp.Models.Plantuml.Constants;
using System.Text;

namespace C4Sharp.Models.Plantuml.Extensions;

/// <summary>
/// Parser Diagram to PlantUML
/// </summary>
public static class PlantumlDiagram
{
    /// <summary>
    /// Create PUML content from Diagram
    /// </summary>
    /// <param name="diagram"></param>
    /// <returns></returns>
    public static string ToPumlString(this Diagram diagram) =>
        ToPumlString(diagram, false);

    /// <summary>
    /// Create PUML content from Diagram
    /// </summary>
    /// <param name="diagram"></param>
    /// <param name="useStandardLibrary"></param>
    /// <returns></returns>
    public static string ToPumlString(this Diagram diagram, bool useStandardLibrary) => new StringBuilder()
            .BuildHeader(diagram, useStandardLibrary)
            .BuildBody(diagram)
            .BuildFooter(diagram)
            .ToString();

    /// <summary>
    /// This call updates the default style of the elements (component, ...) and creates no additional legend entry.
    /// perform this PUML comandUpdate ElementStyle(elementName, ?bgColor, ?fontColor, ?borderColor, ?shadowing)
    /// </summary>
    /// <param name="diagram"></param>
    /// <param name="elementName">(component, person, container,...)</param>
    /// <param name="bgColor">background color</param>
    /// <param name="fontColor">font color</param>
    /// <param name="borderColor">border color</param>
    /// <param name="shadowing">shadowing</param>
    /// <param name="shape"></param>              
    public static Diagram UpdateElementStyle(this Diagram diagram, ElementName elementName, string bgColor, string fontColor, string borderColor, bool shadowing, Shape? shape = null)
    {
        if (elementName is null)
            throw new ArgumentNullException(nameof(elementName), $"{nameof(elementName)} is required");

        var shapeValue = shape is null ? string.Empty : $", ?shape={shape.Value}";
        var item = $"UpdateElementStyle(\"{elementName}\", $bgColor={bgColor}, $fontColor={fontColor}, $borderColor={borderColor}, $shadowing=\"{shadowing}\"{shapeValue} )";
        //diagram.UpdateElementStyle(elementName.Name, item);
        return diagram;
    }

    private static StringBuilder BuildHeader(this StringBuilder stream, Diagram diagram, bool useStandardLibrary)
    {
        var path = GetPumlFilePath(diagram, useStandardLibrary);
        stream.AppendLine($"@startuml {diagram.Slug()}");
        stream.AppendLine($"!include {path}");
        stream.AppendLine();

        BuildStyleSession(stream, diagram);

        if (diagram.LayoutWithLegend && !diagram.ShowLegend)
        {
            stream.AppendLine("LAYOUT_WITH_LEGEND()");
        }

        if (diagram.LayoutAsSketch)
        {
            stream.AppendLine("LAYOUT_AS_SKETCH()");
        }

        stream.AppendLine("SHOW_PERSON_PORTRAIT()");
        stream.AppendLine($"{(diagram.FlowVisualization == DiagramLayout.TopDown ? "LAYOUT_TOP_DOWN()" : "LAYOUT_LEFT_RIGHT()")}");
        stream.AppendLine();

        if (!string.IsNullOrWhiteSpace(diagram.Title))
        {
            stream.AppendLine($"title {diagram.Title}");
            stream.AppendLine();
        }

        return stream;
    }

    private static void BuildStyleSession(StringBuilder stream, Diagram diagram)
    {
        if (diagram.Tags is not null)
        {
            foreach (var (_, value) in diagram.Tags.Items)
            {
                stream.AppendLine(value);
            }

            stream.AppendLine();
        }

        if (diagram.Style is not null)
        {
            foreach (var (_, value) in diagram.Style.Items)
            {
                stream.AppendLine(value);
            }

            stream.AppendLine();
        }

        if (diagram.RelTags is not null)
        {
            foreach (var (_, value) in diagram.RelTags.Items)
            {
                stream.AppendLine(value);
            }

            stream.AppendLine();
        }
    }

    private static StringBuilder BuildBody(this StringBuilder stream, Diagram diagram)
    {
        foreach (var structure in diagram.Structures)
        {
            stream.AppendLine(structure.ToPumlString());
        }

        stream.AppendLine();

        foreach (var relationship in diagram.Relationships)
        {
            stream.AppendLine(relationship.ToPumlString());
        }

        return stream;
    }

    private static StringBuilder BuildFooter(this StringBuilder stream, Diagram diagram)
    {
        if (diagram.ShowLegend)
        {
            stream.AppendLine();
            stream.AppendLine("SHOW_LEGEND()");
        }

        stream.AppendLine("@enduml");

        return stream;
    }

    private static string GetPumlFilePath(this Diagram diagram, bool useUrlInclude)
    {
        const string standardLibraryBaseUrl = "https://raw.githubusercontent.com/plantuml-stdlib/C4-PlantUML/master";
        var pumlFileName = $"{diagram.Name}.puml";

        return useUrlInclude
            ? $"{standardLibraryBaseUrl}/{pumlFileName}"
            : Path.Join(C4SharpDirectory.ResourcesFolderName, pumlFileName);
    }
}
