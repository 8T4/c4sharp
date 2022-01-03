using C4Sharp.Extensions;

namespace C4Sharp.Models;

public record StructureIdentity
{
    private string Key { get; }
    private string? Instance { get; }
    public string Value => Instance is null ? Key : $"{Key}.{Instance.GenerateSlug(".")}";


    public StructureIdentity(string key, string? instance = null) => (Key, Instance) = (key, instance);
    public static StructureIdentity New<T>() => new(typeof(T));
    public static StructureIdentity New<T>(string instance) => new(typeof(T), instance);
    private StructureIdentity(Type type) => (Key, Instance) = (type.FullName!, null);
    private StructureIdentity(Type type, string instance) => (Key, Instance) = (type.FullName!, instance);
}