using System.Collections.Generic;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models
{
    /// <summary>
    /// Container Boundary
    /// </summary>
    public class ContainerBoundary: Structure
    {
        public IEnumerable<Component> Components { get; set; }
        public IEnumerable<Relationship> Relationships { get; set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="alias">Should  be unique</param>
        /// <param name="label"></param>
        public ContainerBoundary(string alias, string label) : base(alias, label)
        {
        }
    }
}