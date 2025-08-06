using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Diagrams;

/// <summary>
/// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
/// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
/// </summary>
public record Diagram(DiagramType Type)
{
    internal string Name { get; } = Type.Value;
    public bool LayoutWithLegend { get; init; } = true;
    public bool ShowLegend { get; init; } = false;
    public bool LayoutAsSketch { get; init; } = false;
    public string? Title { get; init; }
    public string? Description { get; init; } = string.Empty;
    public DiagramLayout FlowVisualization { get; init; } = DiagramLayout.TopDown;
    public IEnumerable<Structure> Structures { get; init; } = [];
    public IEnumerable<Relationship> Relationships { get; init; } = [];
    public IElementStyle? Style { get; init; }
    public IBoundaryStyle? BoundaryStyle { get; init; }
    public IElementTag? Tags { get; init; }
    public IRelationshipTag? RelTags { get; init; }
}
