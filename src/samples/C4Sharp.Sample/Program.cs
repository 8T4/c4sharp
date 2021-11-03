using System;
using C4Sharp.Diagrams;
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

            new PlantumlSession()
                .UseDiagramImageBuilder()
                .UseStandardLibraryBaseUrl()
                .Export(diagrams);
            
            // var path = "/Users/yanjustino/Projects/org.8t4/c4sharp/docs";
            // PlantumlFile.Export(diagrams, path, session);
        }
    }
}