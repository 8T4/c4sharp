using C4Sharp.Extensions;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Diagrams
{
    /// <summary>
    /// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
    /// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
    /// </summary>
    public abstract class Diagram
    {
        internal string Name { get; }
        public bool LayoutWithLegend { get; set; }
        public string Title { get; set; }
        public Structure[] Structures { get; set; }
        public Relationship[] Relationships { get; set; }

        public string Slug() =>
            $"{Title}-{Name}".GenerateSlug();

        protected Diagram(string name)
        {
            LayoutWithLegend = true;
            Name = name;
        }
    }
}