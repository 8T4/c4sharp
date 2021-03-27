using System;

namespace C4Sharp.Models.Relationships
{
    public class Relationship
    {
        public string From { get; }
        public string To { get; }
        public string Label { get; }
        public string Protocol { get; }
        public RelationshipDirection Direction { get; }
        public bool IsBidirectional { get; }

        public Relationship(string @from, string to, string label, string protocol = "", bool isBidirectional = false, RelationshipDirection direction = RelationshipDirection.None)
        {
            From = @from;
            To = to;
            Label = label;
            Protocol = protocol;
            IsBidirectional = isBidirectional;
            Direction = direction;
        }
        
        public Relationship(Structure @from, Structure to, string label, string protocol = "", bool isBidirectional = false, RelationshipDirection direction = RelationshipDirection.None)
        {
            From = @from.Alias;
            To = to.Alias;
            Label = label;
            Protocol = protocol;
            IsBidirectional = isBidirectional;
            Direction = direction;
        }

        public override string ToString()
        {
            var direction = Direction switch
            {
                RelationshipDirection.Down => "Rel_D",
                RelationshipDirection.Up => "Rel_U",
                RelationshipDirection.Left => "Rel_L",
                RelationshipDirection.Right => "Rel_R",
                RelationshipDirection.None => "Rel",
                _ => throw new ArgumentOutOfRangeException()
            };

            return string.IsNullOrEmpty(Protocol)
                ? $"{(IsBidirectional ? "Bi" : "")}{direction}({From}, {To}, \"{Label}\")"
                : $"{(IsBidirectional ? "Bi" : "")}{direction}({From}, {To}, \"{Label}\", \"{Protocol}\" )";
        }        

    }
}