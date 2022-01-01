using C4Sharp.Models.Relationships;

namespace C4Sharp.Models;

/// <summary>
/// A person represents one of the human users of your software system (e.g. actors, roles, personas, etc)
/// <see href="https://c4model.com/"/>
/// </summary>
public sealed record Person : Structure
{
    public Person(string alias, string label) : base(alias, label)
    {
    }

    public Person(string alias, string label, string description) : this(alias, label)
    {
        Description = description;
    }

    public Person(string alias, string label, string description, Boundary boundary) : this(alias, label)
    {
        Description = description;
        Boundary = boundary;
    }
}