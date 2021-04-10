using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C4Sharp.Extensions;
using C4Sharp.Graphic;

namespace C4Sharp.Models.SVG
{
    internal static class SvgStructure
    {
        public static Shape ToSvg(this Structure structure)
        {
            return structure switch
            {
                Person person => person.ToSvg(),
                SoftwareSystem system => system.ToSvg(),
                SoftwareSystemBoundary softwareSystemBoundary => null,
                DeploymentNode deploymentNode => null,
                Component component => null,
                Container container => null,
                ContainerBoundary containerBoundary => null,
                _ => null
            };
        }

        private static Shape ToSvg(this SoftwareSystem system)
        {
            var phrase = system.Description.ToParagraph(30);
            var shape = new Shape(system.Alias, "software_system.svg");

            return shape
                .Fill("#1368bd")
                .Resize(328, 268)
                .Replace("{alias}", system.Alias)
                .Replace("{label}", system.Label)
                .Replace("{line 1}", phrase[0])
                .Replace("{line 2}", phrase[1])
                .Replace("{line 3}", phrase[2]);
        }

        private static Shape ToSvg(this Person person)
        {
            var phrase = person.Description.ToParagraph(30);
            var shape = new Shape(person.Alias, "software_system.svg");

            return shape
                .Fill("#1368bd")
                .Resize(328, 268)
                .Replace("{alias}", person.Alias)
                .Replace("{label}", person.Label)
                .Replace("{line 1}", phrase[0])
                .Replace("{line 2}", phrase[1])
                .Replace("{line 3}", phrase[2]);
        }
    }
}