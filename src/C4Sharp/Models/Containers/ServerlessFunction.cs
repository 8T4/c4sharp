namespace C4Sharp.Models.Containers;

public record ServerlessFunction<T>(string Technology, string Description)
    : Container<T>(ContainerType.ServerlessFunction, Technology, Description);