using C4Sharp.Diagrams.Plantuml;
using C4Sharp.Sample.Diagrams;

namespace C4Sharp.Sample;

internal static class Program
{
    private static void Main()
    {
        var diagrams = new[]
        {
            new ContextDiagram().Build(),
            new ContainerDiagram().Build(),
            new ComponentDiagram().Build(),
            new DeploymentDiagram().Build(),
            new EnterpriseDiagram().Build(),
            new SequenceDiagram().Build()
        };
        
        var context = new PlantumlContext();
        
        context
            .UseDiagramImageBuilder()
            //.UseDiagramSvgImageBuilder()
            //.UseDiagramMermaidBuilder()
            //.UseStandardLibraryBaseUrl() //load the resources from github C4plantuml repository
            .Export(diagrams);
    }
}