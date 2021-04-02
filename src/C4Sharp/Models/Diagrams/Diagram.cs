using System.IO;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models.Plantuml;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Diagrams
{
    /// <summary>
    /// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
    /// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
    /// </summary>
    public abstract class Diagram
    {
        private string PumlFileReference { get; }
        private bool LayoutWithLegend { get; set; }
        public string Title { get; set; }
        public Structure[] Structures { get; set; }
        public Relationship[] Relationships { get; set; }

        public string Slug() =>
            $"{Title}-{PumlFileReference}".GenerateSlug();

        protected Diagram(string pumlFileReference)
        {
            LayoutWithLegend = true;
            PumlFileReference = pumlFileReference;
        }

        public override string ToString()
        {
            var path = Path.Join("..","bin", $"{PumlFileReference}.puml");
                 
            var stream = new StringBuilder();
            stream.AppendLine($"@startuml {Slug()}");
            stream.AppendLine($"!include {path}");
            stream.AppendLine();
            stream.AppendLine($"{(LayoutWithLegend ? "LAYOUT_WITH_LEGEND()" : "")}");
            stream.AppendLine();
     
            foreach (var structure in Structures)
                stream.AppendLine(structure.ToPumlString());
     
            stream.AppendLine();
     
            foreach (var relationship in Relationships)
                stream.AppendLine(relationship.ToPumlString());
     
            stream.AppendLine($"@enduml");
            return stream.ToString();
        }
    }
}