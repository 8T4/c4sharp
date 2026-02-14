using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using HealthcareArchitecture.Converters;
using HealthcareArchitecture.Models;

namespace HealthcareArchitecture.Diagrams;

public class HealthcareContainerDiagram : ContainerDiagram
{
    private readonly ConfigModel _config;

    public HealthcareContainerDiagram(ConfigModel config)
    {
        _config = config;
    }

    public HealthcareContainerDiagram() : this(JsonToC4Converter.LoadConfig("config.json"))
    {
    }

    protected override string Title => _config.Title + " - Container View";

    protected override IEnumerable<Structure> Structures => 
        JsonToC4Converter.ConvertStructures(_config.Structures);

    protected override IEnumerable<Relationship> Relationships => 
        JsonToC4Converter.ConvertRelationships(_config.Relationships);
}
