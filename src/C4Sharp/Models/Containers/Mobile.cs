namespace C4Sharp.Models.Containers;

public record Mobile<T>(string Technology, string Description)
    : Container<T>(ContainerType.Mobile, Technology, Description);