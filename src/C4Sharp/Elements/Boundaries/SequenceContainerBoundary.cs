using C4Sharp.Commons.Extensions;

namespace C4Sharp.Elements.Boundaries;

/// <summary>
/// Container Boundary
/// </summary>
public record SequenceContainerBoundary: Structure, IBoundary
{
    public SequenceContainerBoundary(string alias, string label):base(alias, label)
    {
    }
    
    public SequenceContainerBoundary(StructureIdentity alias, string label):base(alias, label)
    {
    }
    
    public IEnumerable<Component> Components { get; init; } = Array.Empty<Component>();
    public Structure[] GetBoundaryStructures() => Components.Select(x => x as Structure).ToArray();
}

/// <summary>
/// Container Boundary
/// </summary>
public sealed record SequenceContainerBoundary<T> : ContainerBoundary
{
    public SequenceContainerBoundary():base(StructureIdentity.New<T>(), typeof(T).ToNamingConvention())
    {
    }
    
    public SequenceContainerBoundary(string label):base(StructureIdentity.New<T>(), label)
    {
    }
}
