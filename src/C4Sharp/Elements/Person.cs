using C4Sharp.Elements.Relationships;

namespace C4Sharp.Elements;

/// <summary>
/// A person represents one of the human users of your software system (e.g. actors, roles, personas, etc)
/// <see href="https://c4model.com/"/>
/// </summary>
public sealed record Person(string Alias, string Label) : Structure(Alias, Label)
{
    public static Person None => new("none", "None");
    
    public Person(string alias, string label, string description) : this(alias, label)
    {
        Description = description;
    }

    public Person(string alias, string label, string description, Boundary boundary) : this(alias, label)
    {
        Description = description;
        Boundary = boundary;
    }
    
    public static Person operator |(Person a, Boundary boundary) => a with{ Boundary = boundary };
    
    public static Person operator |(Person a, (string alias, string label) b) => new (b.alias, b.label)
    {
        Boundary = a.Boundary
    };
    
    public static Person operator |(Person a, (string alias, string label, string description) b) => new (b.alias, b.label, b.description)
    {
        Boundary = a.Boundary
    };
}