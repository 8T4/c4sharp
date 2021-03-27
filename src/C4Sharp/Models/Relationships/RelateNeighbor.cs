namespace C4Sharp.Models.Relationships
{
    public class RelateNeighbor : Relationship
    {
        public RelateNeighbor(Structure @from, Structure to, string label, string protocol = "") 
            : base(@from, to, label, protocol)
        {
        }        

        public override string ToString()
        {
            return string.IsNullOrEmpty(Protocol)
                ? $"Rel_Neighbor({From}, {To}, \"{Label}\")"
                : $"Rel_Neighbor({From}, {To}, \"{Label}\", \"{Protocol}\" )";
        }
    }
}