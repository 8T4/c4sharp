using System.Collections.Generic;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models.Relationships;
using static C4Sharp.Extensions.StringMethods;

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
    }
}