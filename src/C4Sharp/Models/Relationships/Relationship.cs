using System;
using System.Diagnostics;

namespace C4Sharp.Models.Relationships
{
    public class Relationship
    {
        private string From { get; }
        private string To { get; }
        private string Label { get; set; }
        private string Protocol { get; set; }
        private Position Position { get; set; }
        private Direction Direction { get; }
        
        public Relationship this[string label]
        {
            get
            {
                this.Label = label;
                return this;
            }
        }
        
        public Relationship this[Position position]
        {
            get
            {
                this.Position = position;
                return this;
            }
        }        
        
        public Relationship this[string label, string protocol]
        {
            get
            {
                this.Label = label;
                this.Protocol = protocol;
                return this;
            }
        }        

        public Relationship(Structure @from, Direction direction, Structure to, string label,
            string protocol, Position position = Position.None)
        {
            From = @from.Alias;
            To = to.Alias;
            Label = label;
            Direction = direction;
            Protocol = protocol;
            Position = position;
        }

        public Relationship(Structure @from, Structure to, string label, Position position = Position.None)
            : this(from, Direction.Forward, to, label, string.Empty, position)
        {
        }
        
        public Relationship(Structure @from, Structure to, string label, string protocol, Position position = Position.None)
            : this(from, Direction.Forward, to, label, protocol, position)
        {
        }        
        
        public Relationship(Structure @from, Direction direction, Structure to, string label)
            : this(from, direction, to, label, string.Empty, Position.None)
        {
        }       
        
        public Relationship(Structure @from, Direction direction, Structure to, string label, Position position = Position.None)
            : this(from, direction, to, label, string.Empty, position)
        {
        }         
        
        public Relationship(Structure @from, Direction direction, Structure to, string label, string protocol)
            : this(from, direction, to, label, protocol, Position.None)
        {
        }

        public override string ToString()
        {
            var direction = Direction switch
            {
                Direction.Back => "Rel_Back",
                Direction.Forward => "Rel",
                Direction.Bidirectional => "BiRel",
                _ => "Rel"
            };

            direction += Position switch
            {
                Position.Down => "_D",
                Position.Up => "_U",
                Position.Left => "_L",
                Position.Right => "_R",
                Position.Neighbor => "_Neighbor",
                Position.None => "",
                _ => ""
            };

            return string.IsNullOrEmpty(Protocol)
                ? $"{direction}({From}, {To}, \"{Label}\")"
                : $"{direction}({From}, {To}, \"{Label}\", \"{Protocol}\" )";
        }
    }
}