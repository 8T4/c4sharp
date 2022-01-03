namespace C4Sharp.Models.Containers;

public record ClientDesktop(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.ClientDesktop, Technology, Description);

public record ClientDesktop<T>(string Technology, string? Description = null)
    : Container<T>(ContainerType.ClientDesktop, Technology, Description);