using C4Sharp.Models.Relationships;

namespace C4Sharp.Models;

/// <summary>
/// Container Boundary
/// </summary>
public sealed record ContainerBoundary(string Alias, string Label) : Structure(Alias, Label), IBoundary
{
    public IEnumerable<Component> Components { get; init; } = Array.Empty<Component>();
    public IEnumerable<Relationship> Relationships { get; init; } = Array.Empty<Relationship>();
    public Structure[] GetBoundaryStructures() => Components.Select(x => x as Structure).ToArray();
}
