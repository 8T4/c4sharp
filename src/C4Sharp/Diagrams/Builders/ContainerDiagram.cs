using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;

namespace C4Sharp.Diagrams.Builders;

public abstract class ContainerDiagram: DiagramBuilder
{
    protected override string Title { get; } = "Container Diagram";
    protected override DiagramType DiagramType { get; } = DiagramType.Container;
    
    protected SoftwareSystemBoundary Boundary(string tag, string name, params Container[] components) 
        => new(tag, name, components);
}