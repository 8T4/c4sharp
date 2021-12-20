using System;
using System.Collections.Generic;
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
        public bool LayoutWithLegend { get; init; }
        public bool ShowLegend { get; init; }
        public bool LayoutAsSketch { get; init; }
        public string? Title { get; init; }
        public DiagramLayout FlowVisualization { get; init; }
        public Structure[] Structures { get; init; }
        public Relationship[] Relationships { get; init; }
        public ElementStyle? Style { get; init; }
        public ElementTag? Tags { get; init; } = default;
        public IDictionary<string, string> RelTags { get; private set; }

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
            RelTags = new Dictionary<string, string>();
        }

        public Diagram SetStyle(ElementStyle style) => this with { Style = style};
        public Diagram SetTags(ElementTag tag) => this with { Tags = tag};

        /// <summary>
        /// Introduces a new relation tag. The styles of the tagged relations are updated and the
        /// tag is displayed in the calculated legend.
        /// </summary>
        /// <param name="key">Rel. tag key</param>
        /// <param name="value">Rel. tag value</param>        
        internal void AddRelTag(string key, string value)
        {
            RelTags[key] = value;
        }

        /// <summary>
        /// Slugfy "title-name"
        /// </summary>
        /// <returns></returns>
        public string Slug() =>
            $"{Title}-{Name}".GenerateSlug();        
    }
}