using C4Sharp.Diagrams;
using C4Sharp.Models.Plantuml.IO;
using C4Sharp.Sample.Diagrams;

namespace C4Sharp.Sample
{
    internal static class Program
    {
        private static void Main()
        {
            var diagrams = new Diagram[]
            {
                ContextDiagramBuilder.Build(),
                ContainerDiagramBuilder.Build(),
                ComponentDiagramBuilder.Build(),
                DeploymentDiagramBuilder.Build(),
                EnterpriseDiagramBuilder.Build(),
            };

            new PlantumlSession()
                .UseDiagramImageBuilder()
                .UseDiagramSvgImageBuilder()
                //.UseStandardLibraryBaseUrl()
                .Export(diagrams);
        }
    }
}