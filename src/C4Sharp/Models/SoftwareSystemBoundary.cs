namespace C4Sharp.Models;

/// <summary>
/// Software System Boundary
/// </summary>
public sealed record SoftwareSystemBoundary(string Alias, string Label) : Structure(Alias, Label)
{
    public IEnumerable<Container> Containers { get; init; } = Array.Empty<Container>();
}
