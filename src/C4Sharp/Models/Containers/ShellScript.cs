namespace C4Sharp.Models.Containers;

public record ShellScript(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.ShellScript, Technology, Description);

public record ShellScript<T>(string Technology, string? Description = null)
    : Container<T>(ContainerType.ShellScript, Technology, Description);