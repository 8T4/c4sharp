namespace C4Sharp.Models.Containers;

public record Api<T>(string Technology, string Description)
    : Container<T>(ContainerType.Api, Technology, Description);