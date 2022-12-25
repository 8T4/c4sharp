namespace C4Sharp.Elements;

public record EnterpriseBoundary(string Alias, string Label, params Structure[] Structures) : Structure(Alias, Label), IBoundary
{
    public Structure[] GetBoundaryStructures() => Structures;

}
