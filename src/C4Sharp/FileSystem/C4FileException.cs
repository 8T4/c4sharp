using System.Diagnostics.CodeAnalysis;

namespace C4Sharp.FileSystem;

/// <summary>
/// C4File Exception
/// </summary>
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
}
