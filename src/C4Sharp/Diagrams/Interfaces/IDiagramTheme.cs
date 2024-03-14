namespace C4Sharp.Diagrams.Interfaces;

public interface IDiagramTheme
{
    public IElementStyle? Style { get; }
    public IBoundaryStyle? BoundaryStyle { get; }
    public IElementTag? Tags { get; }
    public IRelationshipTag? RelTags { get; }
}