using System.Collections.Generic;
using System.Text;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// Container Boundary
    /// </summary>
    public class ContainerBoundary: Structure
    {
        public ICollection<Component> Components { get; set; }
        public ICollection<Relationship> Relationships { get; set; }
        
        public ContainerBoundary(string alias, string label) : base(alias, label)
        {
        }
        
        public override string ToString()
        {
            var stream = new StringBuilder();

            stream.AppendLine();
            stream.AppendLine($"Container_Boundary({Alias}, \"{Label}\") {{");
            foreach (var component in Components)
                stream.AppendLine($"    {component}");

            stream.AppendLine();
            
            foreach (var relationship in Relationships)
                stream.AppendLine($"    {relationship}");            

            stream.AppendLine("}");

            return stream.ToString();
        }
    }
}