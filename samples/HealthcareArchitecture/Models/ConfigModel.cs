using Newtonsoft.Json;

namespace HealthcareArchitecture.Models;

public class ConfigModel
{
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    [JsonProperty("slug")]
    public string Slug { get; set; } = string.Empty;

    [JsonProperty("structures")]
    public List<StructureConfig> Structures { get; set; } = new();

    [JsonProperty("relationships")]
    public List<RelationshipConfig> Relationships { get; set; } = new();
}

public class StructureConfig
{
    [JsonProperty("alias")]
    public string Alias { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("technology")]
    public string? Technology { get; set; }

    [JsonProperty("boundary")]
    public string? Boundary { get; set; }

    [JsonProperty("external")]
    public bool External { get; set; } = false;
}

public class RelationshipConfig
{
    [JsonProperty("from")]
    public string From { get; set; } = string.Empty;

    [JsonProperty("to")]
    public string To { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("protocol")]
    public string? Protocol { get; set; }

    [JsonProperty("direction")]
    public string Direction { get; set; } = "Forward";
}
