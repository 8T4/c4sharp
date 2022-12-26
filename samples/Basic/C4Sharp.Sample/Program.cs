using C4Sharp.Elements.Plantuml.IO;
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
        };
        
        var context = new PlantumlContext();
        
        context
            .UseDiagramImageBuilder()
            .UseDiagramSvgImageBuilder()
            //.UseStandardLibraryBaseUrl() //load the resources from github C4plantuml repository
            .Export(diagrams);
    }
}