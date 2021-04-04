namespace C4Sharp.Extensions
{
    /// <summary>
    /// Ident using spaces
    /// </summary>
    internal static class SpaceMethods
    {
        internal static int TabSize => 4;

        internal static string Indent()
        {
            return string.Empty.PadLeft(TabSize);
        }        
        
        internal static string Indent(int size)
        {
            return string.Empty.PadLeft(size);
        }
    }
}