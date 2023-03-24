using C4Sharp.Commons.Extensions;

namespace C4Sharp.Diagrams;

public static class DiagramNamingConvention
{
    public static string PumlFileName(this Diagram diagram) => $"{diagram.Slug()}.puml";
    public static string MermaidFileName(this Diagram diagram) => $"{diagram.Slug()}.mermaid.md";
    public static string Slug(this Diagram diagram) => $"{diagram.Title}-{diagram.Name}".GenerateSlug();    
}