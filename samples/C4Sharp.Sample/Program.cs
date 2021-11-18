using C4Sharp.Diagrams;
using C4Sharp.Models;
using C4Sharp.Models.Plantuml;
using C4Sharp.Sample.Diagrams;

namespace C4Sharp.Sample
{
    internal static class Program
    {
        private static void Main(string[] args)
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
                .UseStandardLibraryBaseUrl()
                .Export(diagrams);
        }
    }
}