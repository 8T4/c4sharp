using C4Sharp.Diagrams.Builders;
using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using HealthcareArchitecture.Converters;
using HealthcareArchitecture.Models;

namespace HealthcareArchitecture.Diagrams;

public class HealthcareContextDiagram : ContextDiagram
{
    private readonly ConfigModel _config;

    public HealthcareContextDiagram(ConfigModel config)
    {
        _config = config;
    }

    public HealthcareContextDiagram() : this(JsonToC4Converter.LoadConfig("config.json"))
    {
    }

    protected override string Title => _config.Title + " - System Context";

    protected override IEnumerable<Structure> Structures => 
        JsonToC4Converter.ConvertStructures(_config.Structures.Where(s => 
            s.Type.Equals("Person", StringComparison.OrdinalIgnoreCase) || 
            s.Type.Equals("SoftwareSystem", StringComparison.OrdinalIgnoreCase)).ToList());

    protected override IEnumerable<Relationship> Relationships => 
        JsonToC4Converter.ConvertRelationships(_config.Relationships);
}
