using System.Diagnostics;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Models.Plantuml
{
    /// <summary>
    /// Parser Structure to PlantUML
    /// </summary>
    internal static class PlantumlStructure
    {
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
            return person.Boundary == Boundary.External
                ? $"Person_Ext({person.Alias}, \"{person.Label}\", \"{person.Description}\" )"
                : $"Person({person.Alias}, \"{person.Label}\", \"{person.Description}\" )";
        }        
        
        private static string ToPumlString(this SoftwareSystem system)
        {
            var isExternal = system.SoftwareSystemType == SoftwareSystemType.External ||
                             system.Boundary == Boundary.External;
            
            return isExternal
                ? $"System_Ext({system.Alias}, \"{system.Label}\", \"{system.Description}\")"
                : $"System({system.Alias}, \"{system.Label}\", \"{system.Description}\")";
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
            return component.Boundary == Boundary.External
                ? $"Component_Ext({component.Alias}, \"{component.Label}\", \"{component.Technology}\", \"{component.Description}\" )"
                : $"Component({component.Alias}, \"{component.Label}\", \"{component.Technology}\", \"{component.Description}\" )";
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

            return  $"{value}({container.Alias}, \"{container.Label}\", \"{container.Technology}\", \"{container.Description}\" )";
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