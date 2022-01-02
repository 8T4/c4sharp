namespace C4Sharp.Models.Containers;

public record EventStreaming(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.Queue, Technology, Description);

public record EventStreaming<T>(string Technology, string? Description = null) 
    : Container<T>(ContainerType.Queue, Technology, Description);