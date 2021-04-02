namespace C4Sharp.Models.Relationships
{
    public class Relationship
    {
        public string From { get; }
        public string To { get; }
        public string Label { get; set; }
        public string Protocol { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; }
        
        public Relationship this[string label]
        {
            get
            {
                Label = label;
                return this;
            }
        }
        
        public Relationship this[Position position]
        {
            get
            {
                Position = position;
                return this;
            }
        }        
        
        public Relationship this[string label, string protocol]
        {
            get
            {
                Label = label;
                Protocol = protocol;
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
    }
}