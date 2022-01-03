using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace C4Sharp.Extensions;

/// <summary>
/// String methods
/// </summary>
[ExcludeFromCodeCoverage]
internal static class StringMethods
{
    /// <summary>
    /// A slug is a part of the URL when you are accessing a resource.
    /// <example>http://localhosts/posts/best-ways-to-make-seo-better</example>
    /// </summary>
    /// <see href="https://stackoverflow.com/questions/19335215/what-is-a-slug"/>
    /// <param name="phrase">text to slugfy</param>
    /// <param name="separator">default separator is hyphens('-')</param>
    /// <returns>text with slug style</returns>
    internal static string GenerateSlug(this string phrase, string separator = "-")
    {
        var str = phrase.RemoveAccent().ToLower(CultureInfo.InvariantCulture);
        str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
        str = Regex.Replace(str, @"\s+", " ").Trim();
        str = Regex.Replace(str, @"\s", separator);
        return str;
    }
    
    /// <summary>
    /// Split by capitalized words
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    internal static string SplitCapitalizedWords(this string text)
    {
        var regex = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) | (?<=[^A-Z])(?=[A-Z]) | (?<=[A-Za-z])(?=[^A-Za-z])", 
            RegexOptions.IgnorePatternWhitespace);

        return regex.Replace(text, " ");
    }

    /// <summary>
    /// Remove Accent from text
    /// </summary>
    /// <param name="text"></param>
    /// <returns>text without accents</returns>
    private static string RemoveAccent(this string text)
    {
        var normalizedString = text.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (var c in normalizedString)
        {
            var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }
}
