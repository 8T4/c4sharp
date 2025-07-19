namespace C4Sharp.Diagrams.Plantuml.Constants;

public record struct ElementName(string Name)
{
    public static ElementName Person => new("person");
    public static ElementName ExternalPerson => new("external_person");
    public static ElementName System => new("system");
    public static ElementName ExternalSystem => new("external_system");
    public static ElementName Component => new("component");
    public static ElementName ExternalComponent => new("external_component");
    public static ElementName Container => new("container");
    public static ElementName ExternalContainer => new("external_container");
    public static ElementName Enterprise => new("enterprise");
}