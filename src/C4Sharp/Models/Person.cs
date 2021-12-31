namespace C4Sharp.Models;

/// <summary>
/// A person represents one of the human users of your software system (e.g. actors, roles, personas, etc)
/// <see href="https://c4model.com/"/>
/// </summary>
public sealed record Person(string Alias, string Label, string Description = "") : Structure(Alias, Label);
