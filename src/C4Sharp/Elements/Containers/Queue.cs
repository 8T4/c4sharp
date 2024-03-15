using C4Sharp.Elements.Relationships;

namespace C4Sharp.Elements.Containers;

public record Queue(
    string Alias,
    string Label,
    string Technology,
    string? Description = null,
    Boundary Boundary = Boundary.Internal)
    : Container(Alias, Label, ContainerType.Queue, Technology, Description, Boundary);

public record Queue<T>(string Technology, string? Description = null) 
    : Container<T>(ContainerType.Queue, Technology, Description);       