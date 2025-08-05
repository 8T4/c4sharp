using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using System.Collections.Generic;

namespace C4Sharp.Generator
{
    public class ConfigurableDiagram : ContextDiagram
    {
        public static string CurrentTitle { get; set; } = string.Empty;
        public static string CurrentSlug { get; set; } = string.Empty;
        public static IEnumerable<Structure> CurrentStructures { get; set; } = new List<Structure>();
        public static IEnumerable<Relationship> CurrentRelationships { get; set; } = new List<Relationship>();

        protected override string Title => CurrentTitle;
        protected override IEnumerable<Structure> Structures => CurrentStructures;
        protected override IEnumerable<Relationship> Relationships => CurrentRelationships;
    }
}

