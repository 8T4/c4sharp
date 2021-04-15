using System.Collections.Generic;

namespace C4Sharp.Models
{
    public class DeploymentNode : Structure
    {
        public IEnumerable<DeploymentNode> Nodes { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public Container Container { get; set; }

        public DeploymentNode(string alias, string label, string description) 
            : base(alias, label, description)
        {
            Nodes = default;
            Container = default;
            Properties = default;
        }
        
        public DeploymentNode(string alias, Container container) 
            : base(alias, container?.Label, container?.Description)
        {
            Nodes = default;
            Container = container;
            Properties = default;
        }
    }
}