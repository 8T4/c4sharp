using System.Reflection;
using System.Reflection.Metadata;

namespace C4Sharp.Models;

/// <summary>
/// Software System Boundary
/// </summary>
public sealed record SoftwareSystemBoundary(string Alias, string Label, params Container[] Containers) : Structure(Alias, Label), IBoundary
{
    public static SoftwareSystemBoundary New(string label, params Container[] containers)
    {
        return new SoftwareSystemBoundary(Guid.NewGuid().ToString("N"), label, containers);
    }
    
    public Structure[] GetBoundaryStructures() => Containers.Select(x => x as Structure).ToArray();
}

