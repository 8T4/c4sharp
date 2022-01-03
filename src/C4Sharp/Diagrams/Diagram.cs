using C4Sharp.Extensions;
using C4Sharp.Models;
using C4Sharp.Models.Relationships;

namespace C4Sharp.Diagrams;

/// <summary>
/// Visualising this hierarchy of abstractions is then done by creating a collection of Context, Container,
/// Component and (optionally) Code (e.g. UML class) diagrams. This is where the C4 model gets its name from.
/// </summary>
public abstract record Diagram
{
    internal string Name { get; }
    public bool LayoutWithLegend { get; init; }
    public bool ShowLegend { get; init; }
    public bool LayoutAsSketch { get; init; }
    public string? Title { get; init; }
    public DiagramLayout FlowVisualization { get; init; }
    public Structure[] Structures { get; init; }
    public Relationship[] Relationships { get; init; }
    public IElementStyle? Style { get; private init; }
    public IElementTag? Tags { get; private init; }
    public IRelationshipTag? RelTags { get; private init; }

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="type"></param>
    protected Diagram(DiagramType type)
    {
        LayoutWithLegend = true;
        LayoutAsSketch = false;
        ShowLegend = false;
        FlowVisualization = DiagramLayout.TopDown;
        Name = type.Value;
        Structures = Array.Empty<Structure>();
        Relationships = Array.Empty<Relationship>();
    }

    public Diagram SetStyle(IElementStyle? style) => this with { Style = style };
    public Diagram SetTags(IElementTag? tag) => this with { Tags = tag };
    public Diagram SetRelTags(IRelationshipTag? tag) => this with { RelTags = tag };

    /// <summary>
    /// Slugfy "title-name"
    /// </summary>
    /// <returns></returns>
    public string Slug() => $"{Title}-{Name}".GenerateSlug();
}
