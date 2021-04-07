using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C4Sharp.Extensions;

namespace C4Sharp.Models.SVG
{
    internal static class SvgStructure
    {
        public static string ToSvg(this Structure structure)
        {
            return structure switch
            {
                Person person => string.Empty,
                SoftwareSystem system => system.ToSvg(),
                SoftwareSystemBoundary softwareSystemBoundary => string.Empty, 
                DeploymentNode deploymentNode => string.Empty,
                Component component => string.Empty,
                Container container => string.Empty,
                ContainerBoundary containerBoundary => string.Empty,
                _ => string.Empty
            };
        }        
        
        private static string ToSvg(this SoftwareSystem system)
        {
            var resource = ResourceMethods.GetResource("software_system.svg");

            var result = resource
                .Replace("{alias}", system.Alias)
                .Replace("{label}", system.Label);

            var phrase = CreateParagraph(system.Description);

            result = result
                .Replace("{line 1}", phrase[0])
                .Replace("{line 2}", phrase[1])
                .Replace("{line 3}", phrase[2]);

            return result;
        }
        
        private static string[] CreateParagraph(string phrase)
        {
            var tokens = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = string.Empty;
            var index = 0;
            var vector = new string[3];

            foreach (var token in tokens)
            {
                if (index >= 3)
                {
                    break;
                }

                var current = string.Join(' ', result, token);
                
                if (current.Length < 20)
                {
                    result = current;
                    vector[index] = current;
                    continue;
                }

                result = string.Empty;
                vector[index] = current;
                index++;
            }

            return vector;
        }
    }
}