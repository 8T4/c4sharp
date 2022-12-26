namespace C4Sharp.Commons.Extensions;

public static class TypeNameMethods
{
    internal static string ToNamingConvention(this Type type)
    {
        return type.IsInterface && type.Name.StartsWith("I")
            ? type.Name.SplitCapitalizedWords().Remove(0, 1)
            : type.Name.SplitCapitalizedWords();
    }
}