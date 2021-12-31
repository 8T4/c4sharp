namespace C4Sharp.Models.Containers;

public record ServerConsole<T>(string Technology, string Description)
    : Container<T>(ContainerType.ServerConsole, Technology, Description);