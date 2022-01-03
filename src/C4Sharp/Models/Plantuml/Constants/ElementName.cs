namespace C4Sharp.Models.Plantuml.Constants;

public record ElementName
{
    public static ElementName Person => new() { Name = "person" };
    public static ElementName ExternalPerson => new() { Name = "external_person" };
    public static ElementName System => new() { Name = "system" };
    public static ElementName ExternalSystem => new() { Name = "external_system" };
    public static ElementName Component => new() { Name = "component" };
    public static ElementName ExternalComponent => new() { Name = "external_component" };
    public static ElementName Container => new() { Name = "container" };
    public static ElementName ExternalContainer => new() { Name = "external_container" };

    public string Name { get; private init; } = string.Empty;
}
