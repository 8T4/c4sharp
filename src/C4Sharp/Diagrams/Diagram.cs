using C4Sharp.Diagrams.Interfaces;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Diagrams;

/// <summary>
/// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
/// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
/// </summary>
public record Diagram
{
    internal string Name { get; }
    public bool LayoutWithLegend { get; init; }
    public bool ShowLegend { get; init; }
    public bool LayoutAsSketch { get; init; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public DiagramLayout FlowVisualization { get; init; }
    public DiagramType Type { get; }
    public IEnumerable<Structure> Structures { get; init; }
    public IEnumerable<Relationship> Relationships { get; init; }
    public IElementStyle? Style { get; init; }
    public IBoundaryStyle? BoundaryStyle { get; init; }
    public IElementTag? Tags { get; init; }
    public IRelationshipTag? RelTags { get; init; }

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="type"></param>
    public Diagram(DiagramType type)
    {
        Type = type;
        LayoutWithLegend = true;
        Description = string.Empty;
        Title = string.Empty;
        LayoutAsSketch = false;
        ShowLegend = false;
        FlowVisualization = DiagramLayout.TopDown;
        Name = type.Value;
        Structures = Array.Empty<Structure>();
        Relationships = Array.Empty<Relationship>();
    }
}
