namespace C4Sharp.Elements.Containers;

public record Pipeline(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.Pipeline, Technology, Description);

public record Pipeline<T>(string Technology, string? Description = null) 
    : Container<T>(ContainerType.Pipeline, Technology, Description); 