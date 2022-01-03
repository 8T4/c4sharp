namespace C4Sharp.Models.Containers;

public record Queue(string Alias, string Label, string Technology, string? Description = null)
    : Container(Alias, Label, ContainerType.Queue, Technology, Description);
    
public record Queue<T>(string Technology, string? Description = null) 
    : Container<T>(ContainerType.Queue, Technology, Description);       