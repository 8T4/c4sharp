namespace C4Sharp.Models.Containers;

public record FileSystem(string Alias, string Label, string Technology, string Description)
    : Container(Alias, Label, ContainerType.FileSystem, Technology, Description);

public record FileSystem<T>(string Technology, string Description)
    : Container<T>(ContainerType.FileSystem, Technology, Description);