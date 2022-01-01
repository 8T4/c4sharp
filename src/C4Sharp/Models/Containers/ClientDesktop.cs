namespace C4Sharp.Models.Containers;

public record ClientDesktop(string Alias, string Label, string Technology, string Description)
    : Container(Alias, Label, ContainerType.ClientDesktop, Technology, Description);

public record ClientDesktop<T>(string Technology, string Description)
    : Container<T>(ContainerType.ClientDesktop, Technology, Description);