using System.ComponentModel;

namespace C4Sharp.Elements;

/// <summary>
/// Component types
/// </summary>
public enum ComponentType
{
    [Description("Database")]
    Database,

    [Description("Queue")]
    Queue,

    [Description("")]
    None
}