using System.Text;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Diagrams.Plantuml;

/// <summary>
/// Parser Diagram to PlantUML
/// </summary>
public static partial class PlantumlDiagram
{
    /// <summary>
    /// Create PUML content from Diagram
    /// </summary>
    /// <param name="diagram"></param>
    /// <returns></returns>
    public static string ToPumlString(this Diagram diagram) => new StringBuilder()
        .BuildHeader(diagram)
        .BuildBody(diagram)
        .BuildFooter(diagram)
        .ToString();

    private static StringBuilder BuildHeader(this StringBuilder stream, Diagram diagram)
    {
        stream.AppendLine($"@startuml {diagram.Slug()}");
        stream.AppendLine($"!include <C4/{diagram.Name}>");
        stream.AppendLine();

        stream.BuildStyleSession(diagram);

        if (diagram.LayoutWithLegend && !diagram.ShowLegend)
        {
            stream.AppendLine("LAYOUT_WITH_LEGEND()");
        }

        if (diagram.LayoutAsSketch)
        {
            stream.AppendLine("LAYOUT_AS_SKETCH()");
        }

        stream.AppendLine("SHOW_PERSON_PORTRAIT()");
        if (diagram.Type != DiagramType.Sequence)
        {
            stream.AppendLine(
                $"{(diagram.FlowVisualization == DiagramLayout.TopDown ? "LAYOUT_TOP_DOWN()" : "LAYOUT_LEFT_RIGHT()")}");
        }

        stream.AppendLine();

        if (!string.IsNullOrWhiteSpace(diagram.Title))
        {
            stream.AppendLine($"title {diagram.Title}");
            stream.AppendLine();
        }

        return stream;
    }

    private static void BuildStyleSession(this StringBuilder stream, Diagram diagram)
    {
        diagram.Tags?.Items.ToList().ForEach(x => stream.AppendLine(x.Value));
        diagram.Style?.Items.ToList().ForEach(x => stream.AppendLine(x.Value));
        diagram.BoundaryStyle?.Items.ToList().ForEach(x => stream.AppendLine(x.Value));
        diagram.RelTags?.Items.ToList().ForEach(x => stream.AppendLine(x.Value));
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
}

/// <summary>
/// Mermaid methods
/// </summary>
public static partial class PlantumlDiagram
{
    /// <summary>
    /// Create mermaid based on puml content from Diagram
    /// </summary>
    /// <param name="diagram"></param>
    /// <returns></returns>
    public static string ToMermaidString(this Diagram diagram) => new StringBuilder()
        .AppendLine("```mermaid")
        .BuildMermaidHeader(diagram)
        .BuildMermaidBody(diagram)
        //.BuildStyleSession(diagram)
        .AppendLine("```")
        .ToString();

    private static StringBuilder BuildMermaidHeader(this StringBuilder stream, Diagram diagram)
    {
        var diagramType = diagram.Type.Value switch
        {
            DiagramConstants.Context => "C4Context",
            DiagramConstants.Container => "C4Container",
            DiagramConstants.Component => "C4Component",
            DiagramConstants.Deployment => "C4Deployment",
            _ => throw new ArgumentOutOfRangeException()
        };

        stream.AppendLine(diagramType);
        stream.AppendLine();

        if (!string.IsNullOrWhiteSpace(diagram.Title))
        {
            stream.AppendLine($"title {diagram.Title}");
            stream.AppendLine();
        }

        return stream;
    }

    private static StringBuilder BuildMermaidBody(this StringBuilder stream, Diagram diagram)
    {
        foreach (var structure in diagram.Structures)
        {
            stream.AppendLine(structure.ToPumlString());
        }

        stream.AppendLine();

        foreach (var relationship in diagram.Relationships)
        {
            var item = (relationship.Position == Position.Neighbor)
                ? relationship[Position.None]
                : relationship;

            stream.AppendLine(item.ClearTags().ToPumlString());
        }

        return stream;
    }
}