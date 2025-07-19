using C4Sharp.Elements;
using C4Sharp.Elements.Boundaries;

namespace C4Sharp.Diagrams.Builders;

public abstract class SequenceDiagram: DiagramBuilder
{
    protected override string Title => "Sequence Diagram";
    protected override DiagramType DiagramType { get; } = DiagramType.Sequence;
    
    protected static SequenceContainerBoundary Bound(string alias, string label, params Component[] structures) =>
        new(alias, label, structures);
}