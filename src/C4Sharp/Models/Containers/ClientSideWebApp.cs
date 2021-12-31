namespace C4Sharp.Models.Containers;

public record ClientSideWebApp<T>(string Technology, string Description)
    : Container<T>(ContainerType.Spa, Technology, Description);