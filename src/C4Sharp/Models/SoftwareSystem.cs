using C4Sharp.Models.Relationships;

namespace C4Sharp.Models;

/// <summary>
/// A software system is the highest level of abstraction and describes something that delivers value to its users,
/// whether they are human or not. This includes the software system you are modelling, and the other software
/// systems upon which your software system depends (or vice versa). In many cases, a software system is "owned by"
/// a single software development team.
/// <see href="https://c4model.com/"/>
/// </summary>
public sealed record SoftwareSystem : Structure
{
    public SoftwareSystem(string alias, string label) : base(alias, label)
    {
    }
    
    public SoftwareSystem(string alias, string label, string description) : this(alias, label)
    {
        Description = description;
    }
    
    public SoftwareSystem(string alias, string label, string description, Boundary boundary) : this(alias, label)
    {
        Description = description;
        Boundary = boundary;
    }    
}
