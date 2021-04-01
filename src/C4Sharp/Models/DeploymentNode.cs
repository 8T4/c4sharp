using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C4Sharp.Models
{
    public class DeploymentNode : Structure
    {
        private const int TAB_SIZE = 4;

        public ICollection<DeploymentNode> Nodes { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        public Container Container { get; set; }

        public DeploymentNode(string alias, string label, string description) : base(alias, label, description)
        {
            Nodes = default;
            Container = default;
            Properties = default;
        }

        public override string ToString() => GetStream();
        
        private string GetStream(int concat = 0)
        {
            var stream = new StringBuilder();
            var spaces = "".PadLeft(concat);

            if (concat == 0)
                stream.AppendLine();

            if (Properties != null)
                foreach (var (key, value) in Properties)
                    stream.AppendLine($"{spaces}AddProperty(\"{key}\", \"{value}\")");

            stream.AppendLine(Tags is null
                ? $"{spaces}Deployment_Node({Alias}, \"{Label}\", \"{Description}\") {{"
                : $"{spaces}Deployment_Node({Alias}, \"{Label}\", \"{Description}\", $tags=\"{string.Join(',', Tags)}\") {{");

            if (Nodes != null)
                foreach (var node in Nodes)
                    stream.AppendLine($"{node.GetStream(concat + TAB_SIZE)}");

            if (Container != null)
                stream.AppendLine("".PadLeft(concat + TAB_SIZE) + Container.ToString());

            stream.Append(spaces + "}");

            return stream.ToString();
        }
    }
}