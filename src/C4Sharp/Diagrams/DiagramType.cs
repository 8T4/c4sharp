namespace C4Sharp.Diagrams;

public record DiagramType(string Value)
{
    public static DiagramType Component => new (DiagramConstants.Component);
    public static DiagramType Container => new (DiagramConstants.Container);
    public static DiagramType Context => new (DiagramConstants.Context);
    public static DiagramType Deployment => new (DiagramConstants.Deployment);
}