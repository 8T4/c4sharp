using System.Collections.Generic;
using System.Text;

namespace C4Sharp.Models
{
    /// <summary>
    /// Software System Boundary
    /// </summary>
    public class SoftwareSystemBoundary: Structure
    {
        public ICollection<Container> Containers { get; set; } = null;
        
        public SoftwareSystemBoundary(string alias, string label) : base(alias, label)
        {
        }

        public override string ToString()
        {
            var stream = new StringBuilder();
            stream.AppendLine();
            stream.AppendLine($"System_Boundary({Alias}, \"{Label}\") {{");

            foreach (var container in Containers)
                stream.AppendLine($"    {container.ToString()}");

            stream.AppendLine("}");

            return stream.ToString();
        }
    }
}