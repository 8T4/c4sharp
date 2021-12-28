namespace C4Sharp.FileSystem;

/// <summary>
/// Manipulate the C4 folder and their resoucers
/// </summary>
internal static class C4SharpDirectory
{
    /// <summary>
    /// Default Directory Name
    /// </summary>
    public static string DirectoryName => "c4";
    /// <summary>
    /// Default Resource Folder Name
    /// </summary>
    public static string ResourcesFolderName => Path.Join("..", ".c4s");
}
