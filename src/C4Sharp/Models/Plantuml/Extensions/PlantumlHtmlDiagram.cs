using System.Text;
using C4Sharp.Diagrams;
using C4Sharp.Extensions;

namespace C4Sharp.Models.Plantuml.Extensions;

public static class PlantumlHtmlDiagram
{
    public static string ToHtmlPage(this Diagram diagram)
    {
        var content = ResourceMethods.GetResourceContent("C4_Diagram.html");
        var structs = ListStructure(diagram);
        
        return content
            .Replace("{{title}}", diagram.Title)
            .Replace("{{description}}", diagram.Description)
            .Replace("{{diagram-type}}", diagram.Type.Name)
            .Replace("{{image}}", diagram.SvgFileName())
            .Replace("{{structure-items}}", structs)
            .Replace("{{alt}}", diagram.Slug());
    }
    
    public static string ToIndexHtmlPage(this IEnumerable<Diagram> diagrams)
    {
        var content = ResourceMethods.GetResourceContent("index.html");
        var items = ListDiagrams(diagrams);
        
        return content
            .Replace("{{diagrams-items}}", items);
    }    

    private static string ListStructure(Diagram diagram)
    {
        var values = new StringBuilder();
        foreach (var structure in diagram.Structures)
        {
            values.AppendLine("<li>");
            values.Append($"<p class=\"font-bold\">{structure.Label}</p> ");
            values.Append($"<p class=\"text-sm text-sky-300\">{structure.Alias} | {structure.Boundary.ToString()} </p> ");
            values.Append($"<p class=\"text-base\">{structure.Description}</p>");
            values.Append($"</li>");
        }

        return values.ToString();
    }
    
    private static string ListDiagrams(IEnumerable<Diagram> diagrams)
    {
        var values = new StringBuilder();
        foreach (var structure in diagrams)
        {
            values.AppendLine("<li>");
            values.Append("<div class=\"grid grid-cols-3 gap-4 \">");
            values.Append($"<div class=\"border bg-cover rounded-lg bg-no-repeat bg-top\" style=\"background-image: url('{structure.SvgFileName()}'); height: 256px\">&nbsp</div>");
            values.Append("<div class=\"col-span-2 divide-y\">");
            values.Append("<div>");
            values.Append($"<p class=\"font-light text-sky-500 mt-2\">{structure.Title}</p> ");
            values.Append($"<p class=\"text-sm text-sky-300\">{structure.Type.Name} Diagram</p> ");
            values.Append($"</div>");
            values.Append("<div>");
            values.Append($"<p class=\"text-sm mt-2\">{structure.Description}</p>");
            values.Append($"<a class=\"text-pink-500 after:content-['_↗']\" target=\"_blank\" href=\"{structure.HtmlPageName()}\">HTML</a> | ");
            values.Append($"<a class=\"text-pink-500 after:content-['_↗']\" target=\"_blank\" href=\"{structure.SvgFileName()}\">SVG</a> | ");
            values.Append($"<a class=\"text-pink-500 after:content-['_↓']\" target=\"_blank\" href=\"{structure.PumlFileName()}\">PUML</a>");
            values.Append($"</div>");
            values.Append("</div>");
            values.Append("</div>");
            values.Append($"</li>");
        }

        return values.ToString();
    }
    
}