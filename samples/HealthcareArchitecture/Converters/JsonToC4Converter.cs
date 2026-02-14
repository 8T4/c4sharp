using C4Sharp.Elements;
using C4Sharp.Elements.Relationships;
using HealthcareArchitecture.Models;
using Newtonsoft.Json;
using static C4Sharp.Elements.Relationships.Position;

namespace HealthcareArchitecture.Converters;

public class JsonToC4Converter
{
    public static ConfigModel LoadConfig(string configPath)
    {
        var json = File.ReadAllText(configPath);
        return JsonConvert.DeserializeObject<ConfigModel>(json) ?? new ConfigModel();
    }

    public static IEnumerable<Structure> ConvertStructures(List<StructureConfig> structureConfigs)
    {
        var structures = new List<Structure>();

        foreach (var config in structureConfigs)
        {
            Structure structure = config.Type.ToLower() switch
            {
                "person" => CreatePerson(config),
                "softwaresystem" => CreateSoftwareSystem(config),
                "container" => CreateContainer(config),
                "component" => CreateComponent(config),
                "deploymentnode" => CreateDeploymentNode(config),
                _ => CreateSoftwareSystem(config) // Default to SoftwareSystem
            };

            structures.Add(structure);
        }

        return structures;
    }

    public static IEnumerable<Relationship> ConvertRelationships(List<RelationshipConfig> relationshipConfigs)
    {
        var relationships = new List<Relationship>();

        foreach (var config in relationshipConfigs)
        {
            var relationship = new Relationship(config.From, config.To)
            {
                Label = config.Label,
                Protocol = config.Protocol ?? string.Empty
            };

            // Set direction based on config
            relationship = config.Direction.ToLower() switch
            {
                "back" => relationship.Back(),
                "bidirectional" => relationship.Bidirectional(),
                _ => relationship // Default to Forward
            };

            relationships.Add(relationship);
        }

        return relationships;
    }

    private static Person CreatePerson(StructureConfig config)
    {
        var person = Person.None | (config.Alias, config.Label, config.Description);
        
        if (config.External)
        {
            person = person | Boundary.External;
        }

        return person;
    }

    private static SoftwareSystem CreateSoftwareSystem(StructureConfig config)
    {
        var system = SoftwareSystem.None | (config.Alias, config.Label, config.Description);
        
        if (config.External)
        {
            system = system | Boundary.External;
        }

        return system;
    }

    private static Container CreateContainer(StructureConfig config)
    {
        var container = Container.None | (config.Alias, config.Label, config.Technology ?? "Technology", config.Description);
        
        if (config.External)
        {
            container = container | Boundary.External;
        }

        return container;
    }

    private static Component CreateComponent(StructureConfig config)
    {
        var component = Component.None | (config.Alias, config.Label, config.Technology ?? "Technology", config.Description);
        
        if (config.External)
        {
            component = component | Boundary.External;
        }

        return component;
    }

    private static DeploymentNode CreateDeploymentNode(StructureConfig config)
    {
        return DeploymentNode.None | (config.Alias, config.Label, config.Technology ?? "Technology", config.Description);
    }
}
