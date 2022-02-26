namespace C4Sharp.Diagrams;

public record DiagramType(string Value, string Name)
{
    public static DiagramType Component => new (DiagramConstants.Component, nameof(DiagramConstants.Component));
    public static DiagramType Container => new (DiagramConstants.Container, nameof(DiagramConstants.Container));
    public static DiagramType Context => new (DiagramConstants.Context, nameof(DiagramConstants.Context));
    public static DiagramType Deployment => new (DiagramConstants.Deployment, nameof(DiagramConstants.Deployment));
}