using System.Collections.Generic;

namespace C4Sharp.Generator
{
    public class DiagramConfig
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public List<StructureConfig> Structures { get; set; }
        public List<RelationshipConfig> Relationships { get; set; }
    }

    public class StructureConfig
    {
        public string Alias { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Boundary { get; set; }
    }

    public class RelationshipConfig
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Label { get; set; }
        public string Technology { get; set; }
    }
}
