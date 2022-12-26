using C4Sharp.Elements.Relationships;

namespace C4Sharp.Elements;

/// <summary>
/// In order to create these maps of your code, we first need a common set of abstractions to create a ubiquitous
/// language that we can use to describe the static structure of a software system. The C4 model considers the
/// static structures of a software system in terms of containers, components and code. And people use the software
/// systems that we build.
/// <see href="https://c4model.com/"/>
/// </summary>
public abstract record Structure
{
    public string Alias { get; }
    public string Label { get; init; } = "";
    public string Description { get; init; } = string.Empty;
    public IEnumerable<string> Tags { get; init; }
    public Boundary Boundary { get; init; } = Boundary.Internal;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="alias">Should be unique</param>
    /// <param name="label"></param>
    protected Structure(string alias, string label, string[]? tags = null) => 
        (Alias, Label, Tags) = (alias, label, tags ?? Array.Empty<string>());

    protected Structure(StructureIdentity identity, string label, string[]? tags = null) 
        : this(identity.Value, label, tags)
    {
    }

    /// <summary>
    /// Forward relationship
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Relationship operator >(Structure a, Structure b) =>
        new(a, b, "uses");

    /// <summary>
    /// Bidirectional relationship
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Relationship operator >=(Structure a, Structure b) =>
        new(a, Direction.Bidirectional, b, "uses");

    /// <summary>
    /// Bidirectional relationship
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Relationship operator <=(Structure a, Structure b) =>
        new(a, Direction.Bidirectional, b, "uses");

    /// <summary>
    /// Back relationship
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Relationship operator <(Structure a, Structure b) =>
        new(a, Direction.Back, b, "uses");
}