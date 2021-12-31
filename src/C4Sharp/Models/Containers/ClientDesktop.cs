namespace C4Sharp.Models.Containers;

public record ClientDesktop<T>(string Technology, string Description)
    : Container<T>(ContainerType.ClientDesktop, Technology, Description);