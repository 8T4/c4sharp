namespace C4Sharp.Models.Containers;

public record MessageBroker(string Alias, string Label, string Technology, string Description)
    : Container(Alias, Label, ContainerType.Queue, Technology, Description);

public record MessageBroker<T>(string Technology, string Description) 
    : Container<T>(ContainerType.Queue, Technology, Description);    