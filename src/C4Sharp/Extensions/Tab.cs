namespace C4Sharp.Extensions
{
    public static class Tab
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