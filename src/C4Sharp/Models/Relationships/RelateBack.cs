namespace C4Sharp.Models.Relationships
{
    public class RelateBack : Relationship
    {
        public RelateBack(Structure backTo, Structure from, string label, string protocol = "") 
            : base(backTo, from, label, protocol)
        {
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(Protocol)
                ? $"Rel_Back({From}, {To}, \"{Label}\")"
                : $"Rel_Back({From}, {To}, \"{Label}\", \"{Protocol}\" )";
        }
    }
}