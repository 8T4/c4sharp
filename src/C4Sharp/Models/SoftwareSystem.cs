namespace C4Sharp.Models;

/// <summary>
/// A software system is the highest level of abstraction and describes something that delivers value to its users,
/// whether they are human or not. This includes the software system you are modelling, and the other software
/// systems upon which your software system depends (or vice versa). In many cases, a software system is "owned by"
/// a single software development team.
/// <see href="https://c4model.com/"/>
/// </summary>
public sealed record SoftwareSystem(string Alias, string Label) : Structure(Alias, Label);
