using C4Sharp.Extensions;

namespace C4Sharp.Models;

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
public record Component : Structure
{
    public string? Technology { get; init; }
    
    public Component(string alias, string label): base(alias, label)
    {
    }
    
    public Component(StructureIdentity alias, string label): base(alias, label)
    {
    }    

    public Component(string alias, string label, string technology): this(alias, label)
    {
        Technology = technology;
    }
    
    public Component(string alias, string label, string technology, string description): this(alias, label)
    {
        Technology = technology;
        Description = description;
    }    
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
