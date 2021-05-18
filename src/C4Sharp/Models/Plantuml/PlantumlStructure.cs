using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// PlantUML Parser
    /// </summary>
    internal static class PlantumlStructure
    {
        /// <summary>
        /// Parser Structure to PlantUML
        /// </summary>        
        public static string ToPumlString(this Structure structure)
        {
            return structure switch
            {
                Person person => person.ToPumlString(),
                SoftwareSystem system => system.ToPumlString(),
                SoftwareSystemBoundary softwareSystemBoundary => softwareSystemBoundary.ToPumlString(), 
                DeploymentNode deploymentNode => deploymentNode.ToPumlString(),
                Component component => component.ToPumlString(),
                Container container => container.ToPumlString(),
                ContainerBoundary containerBoundary => containerBoundary.ToPumlString(),
                _ => string.Empty
            };
        }
        
        private static string ToPumlString(this Person person)
        {
            var external = person.Boundary == Boundary.External
                ? "_Ext"
                : string.Empty;

            return $"Person{external}({person.Alias}, \"{person.Label}\", \"{person.Description}\", $link=\"{person.Link}\")";
        }        
        
        private static string ToPumlString(this SoftwareSystem system)
        {
            var external = system.Boundary == Boundary.External
                ? "_Ext"
                : string.Empty;

            return $"System{external}({system.Alias}, \"{system.Label}\", \"{system.Description}\", $link=\"{system.Link}\")";
        }        
        
        private static string ToPumlString(this SoftwareSystemBoundary boundary)
        {
            var stream = new StringBuilder();
            stream.AppendLine();
            stream.AppendLine($"System_Boundary({boundary.Alias}, \"{boundary.Label}\") {{");

            foreach (var container in boundary.Containers)
            {
                stream.AppendLine($"{SpaceMethods.Indent()}{container.ToPumlString()}");
            }

            stream.AppendLine("}");

            return stream.ToString();
        }           
        
        private static string ToPumlString(this Component component)
        {
            var external = component.Boundary == Boundary.External
                ? "_Ext"
                : string.Empty;

            return $"Component{external}({component.Alias}, \"{component.Label}\", \"{component.Technology}\", \"{component.Description}\", $link=\"{component.Link}\")";
        }     
        
        private static string ToPumlString(this Container container)
        {
            var external = container.Boundary == Boundary.External
                ? "_Ext"
                : string.Empty;

            var value = container.ContainerType switch
            {
                ContainerType.Database => $"ContainerDb{external}",
                ContainerType.Queue => $"ContainerQueue{external}",
                _ => $"Container{external}"
            };

            return  $"{value}({container.Alias}, \"{container.Label}\", \"{container.Technology}\", \"{container.Description}\", $link=\"{container.Link}\")";
        }

        private static string ToPumlString(this ContainerBoundary boundary)
        {
            var stream = new StringBuilder();

            stream.AppendLine();
            stream.AppendLine($"Container_Boundary({boundary.Alias}, \"{boundary.Label}\") {{");
            foreach (var component in boundary.Components)
            {
                stream.AppendLine($"{SpaceMethods.Indent()}{component.ToPumlString()}");
            }

            stream.AppendLine();
            
            foreach (var relationship in boundary.Relationships)
            {
                stream.AppendLine($"{SpaceMethods.Indent()}{relationship.ToPumlString()}");
            }

            stream.AppendLine("}");

            return stream.ToString();
        }
        
        private static string ToPumlString(this DeploymentNode deployment, int concat = 0)
        {
            var stream = new StringBuilder();
            var spaces = SpaceMethods.Indent(concat);

            if (concat == 0)
            {
                stream.AppendLine();
            }

            if (deployment.Properties != null)
            {
                foreach (var (key, value) in deployment.Properties)
                {
                    stream.AppendLine($"{spaces}AddProperty(\"{key}\", \"{value}\")");
                }
            }

            stream.AppendLine(deployment.Tags is null
                ? $"{spaces}Deployment_Node({deployment.Alias}, \"{deployment.Label}\", \"{deployment.Description}\") {{"
                : $"{spaces}Deployment_Node({deployment.Alias}, \"{deployment.Label}\", \"{deployment.Description}\", $tags=\"{string.Join(',', deployment.Tags)}\") {{");

            if (deployment.Nodes != null)
            {
                foreach (var node in deployment.Nodes)
                {
                    stream.AppendLine($"{node.ToPumlString(concat + SpaceMethods.TabSize)}");
                }
            }

            if (deployment.Container != null)
            {
                stream.AppendLine(SpaceMethods.Indent(concat) + deployment.Container.ToPumlString());
            }

            stream.Append(spaces + "}");

            return stream.ToString();
        }
    }
}