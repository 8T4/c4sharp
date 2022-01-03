namespace C4Sharp.Models.Containers;

public record Database(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.Database, Technology, Description);

public record Database<T>(string Technology, string? Description = null)
    : Container<T>(ContainerType.Database, Technology, Description);