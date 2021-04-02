using System.Collections.Generic;
using System.Text;

namespace C4Sharp.Models
{
    /// <summary>
    /// Software System Boundary
    /// </summary>
    public class SoftwareSystemBoundary: Structure
    {
        public ICollection<Container> Containers { get; set; }
        
        public SoftwareSystemBoundary(string alias, string label) : base(alias, label)
        {
        }
    }
}