using System.ComponentModel;
using C4Sharp.Extensions;

namespace C4Sharp.Models;

/// <summary>
/// Not Docker! In the C4 model, a container represents an application or a data store. A container is something
/// that needs to be running in order for the overall software system to work. In real terms, a container is
/// something like:
///
/// Server-side web application, Client-side web application, Client-side desktop application,
/// Mobile app, Server-side console application, Serverless function, Database, Blob or content store,
/// File system, Shell script
///
/// <see href="https://c4model.com/#ContainerDiagram"/>
/// </summary>
public record Container : Structure
{
    private readonly Dictionary<string, Container> _instances = new();

    public ContainerType ContainerType { get; init; }
    public string? Technology { get; init; }
    public Container this[int index] => GetInstance(index.ToString());
    public Container this[string instanceName] => GetInstance(instanceName);

    public Container(string alias, string label) : base(alias, label)
    {
        ContainerType = ContainerType.None;
        Technology = $"{ContainerType.ToString().SplitCapitalizedWords()}";
    }

    public Container(string alias, string label, ContainerType type, string? technology) : this(alias, label)
    {
        ContainerType = type;
        Technology = technology is null
            ? $"{type.ToString().SplitCapitalizedWords()}"
            : $"{type.ToString().SplitCapitalizedWords()}:{technology}";
    }

    public Container(string alias, string label, ContainerType type, string? technology, string? description)
        : this(alias, label, type, technology)
    {
        Description = description ?? string.Empty;
    }

    /// <summary>
    /// Get or Create a instance of current container
    /// </summary>
    /// <param name="instanceName">instance name</param>
    /// <returns>New Container</returns>
    private Container GetInstance(string instanceName)
    {
        var id = new StructureIdentity(Alias, instanceName);

        _instances.TryGetValue(id.Value, out var instance);

        var container = instance ?? new Container(id.Value, Label)
        {
            ContainerType = ContainerType,
            Description = Description,
            Technology = Technology,
            Boundary = Boundary,
            Tags = Tags
        };

        if (instance is null)
        {
            _instances[id.Value] = container;
        }

        return container;
    }
}

public record Container<T> : Container
{
    private protected Container(ContainerType type, string? technology = null, string? description = null)
        : base(
            StructureIdentity.New<T>().Value,
            typeof(T).ToNamingConvention(),
            type,
            technology,
            description ?? typeof(T).ToNamingConvention()
        ) { }
}