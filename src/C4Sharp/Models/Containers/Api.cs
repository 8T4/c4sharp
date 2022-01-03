
namespace C4Sharp.Models.Containers;

public record Api(string Alias, string Label, string Technology, string? Description = null) 
    : Container(Alias, Label, ContainerType.Api, Technology, Description);

public record Api<T>(string Technology, string? Description = null) 
    : Container<T>(ContainerType.Api, Technology, Description);