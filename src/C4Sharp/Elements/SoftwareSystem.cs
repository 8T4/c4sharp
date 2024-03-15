using C4Sharp.Elements.Relationships;

namespace C4Sharp.Elements;

/// <summary>
/// A software system is the highest level of abstraction and describes something that delivers value to its users,
/// whether they are human or not. This includes the software system you are modelling, and the other software
/// systems upon which your software system depends (or vice versa). In many cases, a software system is "owned by"
/// a single software development team.
/// <see href="https://c4model.com/"/>
/// </summary>
public sealed record SoftwareSystem(string Alias, string Label) : Structure(Alias, Label)
{
    public static SoftwareSystem None => new("none", "None");

    public SoftwareSystem(string alias, string label, string description) : this(alias, label)
    {
        Description = description;
    }
    
    public SoftwareSystem(string alias, string label, string description, Boundary boundary) : this(alias, label)
    {
        Description = description;
        Boundary = boundary;
    }  
    
    public static SoftwareSystem operator |(SoftwareSystem a, Boundary boundary) => a with{ Boundary = boundary};
    
    public static SoftwareSystem operator |(SoftwareSystem a, (string alias, string label) b) => new (b.alias, b.label)
    {
        Boundary = a.Boundary
    };
    
    public static SoftwareSystem operator |(SoftwareSystem a, (string alias, string label, string description) b) => new (b.alias, b.label, b.description)
    {
        Boundary = a.Boundary
    };
    
}
