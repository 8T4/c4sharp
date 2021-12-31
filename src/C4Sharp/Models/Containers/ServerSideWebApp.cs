namespace C4Sharp.Models.Containers;

public record ServerSideWebApp<T>(string Technology, string Description)
    : Container<T>(ContainerType.WebApplication, Technology, Description);