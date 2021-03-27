using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Diagrams
{
    /// <summary>
    /// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
    /// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
    /// </summary>
    public abstract class CoreDiagram
    {
        public string PumlFileReference { get; }
        public string Title { get; set; }
        public bool LayoutWithLegend { get; set; }
        public Structure[] Structures { get; set; }
        public Relationship[] Relationships { get; set; }

        public string Slug() =>
            $"{Title.Replace(" ", "_")}_{PumlFileReference}";

        protected CoreDiagram(string pumlFileReference)
        {
            LayoutWithLegend = true;
            PumlFileReference = pumlFileReference;
        }
    }
}