namespace C4Sharp.Models.Containers;

public record EventStreaming<T>(string Technology, string Description) 
    : Container<T>(ContainerType.Queue, Technology, Description);