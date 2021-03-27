namespace C4Sharp.Models.Relationships
{
    public class RelateBackNeighbor : Relationship
    {
        public RelateBackNeighbor(Structure backTo, Structure from, string label, string protocol = "") 
            : base(backTo, from, label, protocol)
        {
        }
        
        public override string ToString()
        {
            return string.IsNullOrEmpty(Protocol)
                ? $"Rel_Back_Neighbor({From}, {To}, \"{Label}\")"
                : $"Rel_Back_Neighbor({From}, {To}, \"{Label}\", \"{Protocol}\" )";
        }

    }
}