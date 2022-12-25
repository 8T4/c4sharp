
namespace C4Sharp.Elements.Containers;

public record Api(string Alias, string Label, string Technology, string? Description = null) 
    : Container(Alias, Label, ContainerType.Api, Technology, Description);

public record Api<T>(string? Technology = null, string? Description = null) 
    : Container<T>(ContainerType.Api, Technology, Description);