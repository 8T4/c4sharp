using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Diagrams.Builders;

public abstract class ComponentDiagram: DiagramBuilder
{
    protected override string Title { get; } = "Component Diagram";
    protected override DiagramType DiagramType { get; } = DiagramType.Component;
    
    protected ContainerBoundary Boundary(string tag, string name, params Component[] components) 
        => new(tag, name, components);
}