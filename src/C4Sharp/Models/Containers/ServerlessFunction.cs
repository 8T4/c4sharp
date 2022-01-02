namespace C4Sharp.Models.Containers;

public record ServerlessFunction(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.ServerlessFunction, Technology, Description);

public record ServerlessFunction<T>(string Technology, string? Description = null)
    : Container<T>(ContainerType.ServerlessFunction, Technology, Description);