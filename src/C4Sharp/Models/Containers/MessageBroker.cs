namespace C4Sharp.Models.Containers;

public record MessageBroker<T>(string Technology, string Description) 
    : Container<T>(ContainerType.Queue, Technology, Description);    