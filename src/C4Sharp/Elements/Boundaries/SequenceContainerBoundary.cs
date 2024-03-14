using C4Sharp.Commons.Extensions;

namespace C4Sharp.Elements.Boundaries;

/// <summary>
/// Container Boundary
/// </summary>
public record SequenceContainerBoundary(string Alias, string Lablel, params Component[] Components): Structure(Alias, Lablel), IBoundary
{
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
