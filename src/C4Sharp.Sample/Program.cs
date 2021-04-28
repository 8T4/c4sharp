using System;
using C4Sharp.Models.Diagrams;
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
                DeploymentDiagramBuilder.Build()
            };

            using var session = new PlantumlSession();
            PlantumlFile.Export(diagrams, session);
        }
    }
}