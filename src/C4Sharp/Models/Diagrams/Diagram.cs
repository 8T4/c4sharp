using System.IO;
using System.Text;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Diagrams
{
    /// <summary>
    /// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
    /// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
    /// </summary>
    public abstract class Diagram
    {
        public string PumlFileReference { get; }
        public string Title { get; set; }
        public bool LayoutWithLegend { get; set; }
        public Structure[] Structures { get; set; }
        public Relationship[] Relationships { get; set; }

        public string Slug() =>
            $"{Title.Replace(" ", "_")}_{PumlFileReference}";

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
                stream.AppendLine(structure.ToString());
     
            stream.AppendLine();
     
            foreach (var relationship in Relationships)
                stream.AppendLine(relationship.ToString());
     
            stream.AppendLine($"@enduml");
            return stream.ToString();
        }
    }
}