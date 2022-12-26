namespace C4Sharp.Elements.Boundaries;

/// <summary>
/// Software System Boundary
/// </summary>
public sealed record SoftwareSystemBoundary(string Alias, string Label, params Container[] Containers) : Structure(Alias, Label), IBoundary
{
    public Structure[] GetBoundaryStructures() => Containers.Select(x => x as Structure).ToArray();
}

