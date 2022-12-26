using C4Sharp.Commons.Extensions;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Elements.Boundaries;

/// <summary>
/// Container Boundary
/// </summary>
public record ContainerBoundary: Structure, IBoundary
{
    public ContainerBoundary(string alias, string label):base(alias, label)
    {
    }
    
    public ContainerBoundary(StructureIdentity alias, string label):base(alias, label)
    {
    }
    
    public IEnumerable<Component> Components { get; init; } = Array.Empty<Component>();
    public IEnumerable<Relationship> Relationships { get; init; } = Array.Empty<Relationship>();
    public Structure[] GetBoundaryStructures() => Components.Select(x => x as Structure).ToArray();
}

/// <summary>
/// Container Boundary
/// </summary>
public sealed record ContainerBoundary<T> : ContainerBoundary
{
    public ContainerBoundary():base(StructureIdentity.New<T>(), typeof(T).ToNamingConvention())
    {
    }
    
    public ContainerBoundary(string label):base(StructureIdentity.New<T>(), label)
    {
    }
}
