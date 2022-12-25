using System.Text;
using C4Sharp.Diagrams;
using C4Sharp.FileSystem;

namespace C4Sharp.Elements.Plantuml.Extensions;

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
    public static string ToPumlString(this Diagram diagram) => ToPumlString(diagram, false);

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