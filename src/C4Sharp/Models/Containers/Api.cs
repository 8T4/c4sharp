
namespace C4Sharp.Models.Containers;

public record Api(string Alias, string Label, string Technology, string Description)
    : Container(Alias, Label, ContainerType.Api, Technology, Description);

public record Api<T>(string Technology, string Description)
    : Container<T>(ContainerType.Api, Technology, Description);