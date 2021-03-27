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
        private ICollection<Component> Components { get; }
        private ICollection<Relationship> Relationships { get; }
        
        
        public ContainerBoundary(string alias, string label, ICollection<Component> components) : base(alias, label)
        {
            Components = components ?? new List<Component>();
            Relationships = new List<Relationship>();
        }

        public ContainerBoundary(string alias, string label, ICollection<Component> components, ICollection<Relationship> relationships) : base(alias, label)
        {
            Components = components ?? new List<Component>();
            Relationships = relationships ?? new List<Relationship>();
        }
        
        public override string ToString()
        {
            var stream = new StringBuilder();

            stream.AppendLine();
            stream.AppendLine($"Container_Boundary({Alias}, \"{Label}\") {{");
            foreach (var component in Components)
                stream.AppendLine($"    {component.ToString()}");

            stream.AppendLine();
            
            foreach (var relationship in Relationships)
                stream.AppendLine($"    {relationship.ToString()}");            

            stream.AppendLine("}");

            return stream.ToString();
        }
    }
}