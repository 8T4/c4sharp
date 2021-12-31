namespace C4Sharp.Models.Containers;

public record ShellScript<T>(string Technology, string Description)
    : Container<T>(ContainerType.ShellScript, Technology, Description);