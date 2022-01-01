namespace C4Sharp.Models.Containers;

public record ClientSideWebApp(string Alias, string Label, string Technology, string Description)
    : Container(Alias, Label, ContainerType.Spa, Technology, Description);

public record ClientSideWebApp<T>(string Technology, string Description)
    : Container<T>(ContainerType.Spa, Technology, Description);