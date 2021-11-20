using System;
using C4Sharp.Extensions;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Diagrams
{
    /// <summary>
    /// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
    /// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
    /// </summary>
    public abstract record Diagram
    {
        internal string Name { get; }
        public bool LayoutWithLegend { get; set; }
        public bool ShowLegend { get; set; }
        public bool LayoutAsSketch { get; set; }
        public string? Title { get; set; }
        public DiagramLayout FlowVisualization { get; set; }
        public Structure[] Structures { get; set; }
        public Relationship[] Relationships { get; set; }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="name"></param>
        protected Diagram(string name)
        {
            LayoutWithLegend = true;
            LayoutAsSketch = false;
            ShowLegend = false;
            FlowVisualization = DiagramLayout.TopDown;
            Name = name;
            Structures = Array.Empty<Structure>();
            Relationships = Array.Empty<Relationship>();
        }
        
        /// <summary>
        /// Slugfy "title-name"
        /// </summary>
        /// <returns></returns>
        public string Slug() =>
            $"{Title}-{Name}".GenerateSlug();        
    }
}