using System.Collections.Generic;

namespace C4Sharp.Models
{
    /// <summary>
    /// Software System Boundary
    /// </summary>
    public class SoftwareSystemBoundary: Structure
    {
        public IEnumerable<Container> Containers { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should be unique</param>
        /// <param name="label"></param>
        public SoftwareSystemBoundary(string alias, string label) 
            : base(alias, label)
        {
        }
    }
}