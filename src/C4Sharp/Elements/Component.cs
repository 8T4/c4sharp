using C4Sharp.Commons.Extensions;
using C4Sharp.Elements.Relationships;

namespace C4Sharp.Elements;

/// <summary>
/// The word "component" is a hugely overloaded term in the software development industry, but in this context a
/// component is a grouping of related functionality encapsulated behind a well-defined interface. If you're using
/// a language like Java or C#, the simplest way to think of a component is that it's a collection of implementation
/// classes behind an interface. Aspects such as how those components are packaged (e.g. one component vs many
/// components per JAR file, DLL, shared library, etc) is a separate and orthogonal concern.
/// An important point to note here is that all components inside a container typically execute in the same process
/// space. In the C4 model, components are not separately deployable units.
/// <see href="https://c4model.com/"/>
/// </summary>
public record Component(string Alias, string Label) : Structure(Alias, Label)
{
    public string? Technology { get; init; }
    public ComponentType ComponentType { get; init; } = ComponentType.None;
    public static Component None => new("none", "None");
    
    public Component(StructureIdentity alias, string label): this(alias.Value, label)
    {
        ComponentType = ComponentType.None;
    }    

    public Component(string alias, string label, string technology): this(alias, label)
    {
        Technology = technology;
    }    

    public Component(string alias, string label, ComponentType componentType, string technology): this(alias, label, technology)
    {
        ComponentType = componentType;
    }
    
    public Component(string alias, string label, string technology, string description): this(alias, label)
    {
        Technology = technology;
        Description = description;
    }
    
    public Component(string alias, string label, ComponentType componentType, string technology, string description): this(alias, label, technology, description)
    {
        ComponentType = componentType;
    } 
    
    public static Component operator |(Component a, Boundary boundary) => a with{ Boundary = boundary};
    public static Component operator |(Component a, ComponentType b) => a with{ ComponentType = b };
    public static Component operator |(Component a, (string alias, string label) b) => new (b.alias, b.label);
    public static Component operator |(Component a, (string alias, string label, string technology) b) => new Component(b.alias, b.label, b.technology);
    public static Component operator |(Component a, (string alias, string label, string technology , string description) b) => new (b.alias, b.label, b.technology, b.description);    
    
}

public record Component<T> : Component
{
    public Component():base(StructureIdentity.New<T>(), typeof(T).ToNamingConvention())
    {
    }

    public Component(string technology): base(StructureIdentity.New<T>().Value, typeof(T).ToNamingConvention(), technology)
    {
    }
    
    public Component(string technology, string description): base(StructureIdentity.New<T>().Value, typeof(T).ToNamingConvention(), technology, description)
    {
    }     
}
