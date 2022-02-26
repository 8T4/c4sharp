using C4Sharp.Models.Plantuml.IO;
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
        
        new PlantumlContext()
            .UseDiagramImageBuilder()
            .UseDiagramSvgImageBuilder()
            .UseStandardLibraryBaseUrl()
            .UseHtmlPageBuilder()
            .Export(diagrams);
    }
}