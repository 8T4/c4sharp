using System.Text;
using C4Sharp.Commons.FileSystem;
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

    private static StringBuilder BuildStyleSession(this StringBuilder stream, Diagram diagram)
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

        return stream;
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

/// <summary>
/// Mermaid methods
/// </summary>
public static partial class PlantumlDiagram
{
    /// <summary>
    /// Create mermaid based on puml content from Diagram
    /// </summary>
    /// <param name="diagram"></param>
    /// <param name="useStandardLibrary"></param>
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



