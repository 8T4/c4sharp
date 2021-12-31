namespace C4Sharp.Models.Containers;

public record Database<T>(string Technology, string Description)
    : Container<T>(ContainerType.Database, Technology, Description);