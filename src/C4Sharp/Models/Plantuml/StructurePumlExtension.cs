using System.Text;
using C4Sharp.Extensions;

namespace C4Sharp.Models.Plantuml
{
    public static class StructurePumlExtension
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
            return $"Person({person.Alias}, \"{person.Label}\", \"{person.Description}\" )";
        }        
        
        private static string ToPumlString(this SoftwareSystem system)
        {
            return system.SoftwareSystemType == SoftwareSystemType.External
                ? $"System_Ext({system.Alias}, \"{system.Label}\", \"{system.Description}\")"
                : $"System({system.Alias}, \"{system.Label}\", \"{system.Description}\")";
        }        
        
        private static string ToPumlString(this SoftwareSystemBoundary boundary)
        {
            var stream = new StringBuilder();
            stream.AppendLine();
            stream.AppendLine($"System_Boundary({boundary.Alias}, \"{boundary.Label}\") {{");

            foreach (var container in boundary.Containers)
                stream.AppendLine($"{Tab.Indent()}{container.ToPumlString()}");

            stream.AppendLine("}");

            return stream.ToString();
        }           
        
        private static string ToPumlString(this Component component)
        {
            return $"Component({component.Alias}, \"{component.Label}\", \"{component.Technology}\", \"{component.Description}\" )";
        }     
        
        private static string ToPumlString(this Container container)
        {
            return container.ContainerType == ContainerType.Database
                ? $"ContainerDb({container.Alias}, \"{container.Label}\", \"{container.Technology}\", \"{container.Description}\" )"
                : $"Container({container.Alias}, \"{container.Label}\", \"{container.Technology}\", \"{container.Description}\" )";
        }

        private static string ToPumlString(this ContainerBoundary boundary)
        {
            var stream = new StringBuilder();

            stream.AppendLine();
            stream.AppendLine($"Container_Boundary({boundary.Alias}, \"{boundary.Label}\") {{");
            foreach (var component in boundary.Components)
                stream.AppendLine($"{Tab.Indent()}{component.ToPumlString()}");

            stream.AppendLine();
            
            foreach (var relationship in boundary.Relationships)
                stream.AppendLine($"{Tab.Indent()}{relationship.ToPumlString()}");            

            stream.AppendLine("}");

            return stream.ToString();
        }
        
        private static string ToPumlString(this DeploymentNode deployment, int concat = 0)
        {
            var stream = new StringBuilder();
            var spaces = Tab.Indent(concat);

            if (concat == 0)
                stream.AppendLine();

            if (deployment.Properties != null)
                foreach (var (key, value) in deployment.Properties)
                    stream.AppendLine($"{spaces}AddProperty(\"{key}\", \"{value}\")");

            stream.AppendLine(deployment.Tags is null
                ? $"{spaces}Deployment_Node({deployment.Alias}, \"{deployment.Label}\", \"{deployment.Description}\") {{"
                : $"{spaces}Deployment_Node({deployment.Alias}, \"{deployment.Label}\", \"{deployment.Description}\", $tags=\"{string.Join(',', deployment.Tags)}\") {{");

            if (deployment.Nodes != null)
                foreach (var node in deployment.Nodes)
                    stream.AppendLine($"{node.ToPumlString(concat + Tab.TabSize)}");

            if (deployment.Container != null)
                stream.AppendLine(Tab.Indent(concat) + deployment.Container.ToPumlString());

            stream.Append(spaces + "}");

            return stream.ToString();
        }

    }
}