namespace C4Sharp.Models.Containers;

public record ServerSideWebApp(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.WebApplication, Technology, Description);

public record ServerSideWebApp<T>(string Technology, string? Description = null)
    : Container<T>(ContainerType.WebApplication, Technology, Description);