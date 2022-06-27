using System.ComponentModel;

namespace C4Sharp.Models;

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