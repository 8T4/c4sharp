namespace C4Sharp.Extensions
{
    /// <summary>
    /// Indentation methods
    /// </summary>
    internal static class SpaceMethods
    {
        /// <summary>
        /// Default indentation tabsize
        /// </summary>
        internal static int TabSize => 4;

        /// <summary>
        /// Ident string using default tabsize
        /// </summary>
        /// <returns>indented text</returns>
        internal static string Indent()
        {
            return string.Empty.PadLeft(TabSize);
        }        
        
        /// <summary>
        /// Ident string using tabsize
        /// </summary>
        /// <param name="size">tabsize</param>
        /// <returns>indented text</returns>
        internal static string Indent(int size)
        {
            return string.Empty.PadLeft(size);
        }
    }
}