namespace C4Sharp.Models.Containers;

public record ServerConsole(string Alias, string Label, string Technology, string Description)
    : Container(Alias, Label, ContainerType.ServerConsole, Technology, Description);

public record ServerConsole<T>(string Technology, string Description)
    : Container<T>(ContainerType.ServerConsole, Technology, Description);