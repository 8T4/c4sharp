namespace C4Sharp.Models.Containers;

public record FileSystem<T>(string Technology, string Description)
    : Container<T>(ContainerType.FileSystem, Technology, Description);