using System.ComponentModel;

namespace C4Sharp.Extensions;

public static class TypeNameMethods
{
    internal static string ToNamingConvention(this Type type)
    {
        return type.IsInterface && type.Name.StartsWith("I")
            ? type.Name.SplitCapitalizedWords().Remove(0, 1)
            : type.Name.SplitCapitalizedWords();
    }
    
    public static string? GetValueFromDescription(this Type type)
    {
        var description = type.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault(x => x is DescriptionAttribute);

        if (description is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        return null;
    }    
}