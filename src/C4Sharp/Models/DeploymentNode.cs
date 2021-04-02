using System.Collections.Generic;
using System.Text;
using C4Sharp.Extensions;
using static C4Sharp.Extensions.StringMethods;

namespace C4Sharp.Models
{
    public class DeploymentNode : Structure
    {
        public ICollection<DeploymentNode> Nodes { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public Container Container { get; set; }

        public DeploymentNode(string alias, string label, string description) : base(alias, label, description)
        {
            Nodes = default;
            Container = default;
            Properties = default;
        }
    }
}