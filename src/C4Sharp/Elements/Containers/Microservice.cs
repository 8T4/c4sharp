namespace C4Sharp.Elements.Containers;

public record Microservice(string Alias, string Label, string Technology, string? Description = null) 
    : Container(Alias, Label, ContainerType.Microservice, Technology, Description, new []{ "microservice" });

public record Microservice<T>(string? Technology = null, string? Description = null) 
    : Container<T>(ContainerType.Microservice, Technology, Description, new []{ "microservice" });
    
    
    