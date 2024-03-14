using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;

namespace C4Sharp.Diagrams.Builders;

public abstract class ContextDiagram: DiagramBuilder
{
    protected override string Title { get; } = "Context Diagram";
    protected override DiagramType DiagramType { get; } = DiagramType.Component;
    
    protected EnterpriseBoundary Boundary(string alias, string label, params Structure[] structures) =>
        new(alias, label, structures);
}