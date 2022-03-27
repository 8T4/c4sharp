using C4Sharp.Extensions;

namespace C4Sharp.Diagrams;

public static class DiagramNamingConvention
{
    public static string HtmlPageName(this Diagram diagram) => $"{diagram.Reference}.html";
    public static string SvgFileName(this Diagram diagram) => $"{diagram.Slug()}.svg";
    public static string PumlFileName(this Diagram diagram) => $"{diagram.Slug()}.puml";
    public static string Slug(this Diagram diagram) => $"{diagram.Title}-{diagram.Name}".GenerateSlug();    
}