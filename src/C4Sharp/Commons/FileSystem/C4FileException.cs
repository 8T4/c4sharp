using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace C4Sharp.FileSystem;

/// <summary>
/// C4File Exception
/// </summary>
[Serializable]
[ExcludeFromCodeCoverage]
public class C4FileException : Exception
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    public C4FileException(string message) : base(message)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public C4FileException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public C4FileException([NotNull] SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
