using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C4Sharp.Models
{
    public class DeploymentNode : Structure
    {
        private const int TAB_SIZE = 4;


        public ICollection<DeploymentNode> Nodes { get; set; }
        public Container Container { get; set; }

        public DeploymentNode(string alias, string label, string description) : base(alias, label, description)
        {
            Nodes = default;
            Container = default;
        }

        public override string ToString()
        {
            return ToPumlString();
        }

        private string ToPumlString(int concat = 0)
        {
            var stream = new StringBuilder();
            
            if (concat == 0)
                stream.AppendLine();
            
            stream.AppendLine(Tags is null
                ? $"{"".PadLeft(concat)}Deployment_Node({Alias}, \"{Label}\", \"{Description}\") {{"
                : $"{"".PadLeft(concat)}Deployment_Node({Alias}, \"{Label}\", \"{Description}\", $tags=\"{string.Join(',', Tags)}\") {{");

            foreach (var node in Nodes ?? new DeploymentNode[] { })
                stream.AppendLine($"{node.ToPumlString(concat + TAB_SIZE)}");

            if (Container != null)
                stream.AppendLine("".PadLeft(concat + TAB_SIZE) + Container.ToString());

            stream.Append("".PadLeft(concat) + "}");

            return stream.ToString();
        }
    }
}