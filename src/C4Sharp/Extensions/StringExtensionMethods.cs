using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace C4Sharp.Extensions
{
    public static class StringExtensionMethods
    {
        internal static string GenerateSlug(this string phrase) 
        { 
            var str = phrase.RemoveAccent().ToLower(); 
            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); 
            str = Regex.Replace(str, @"\s+", " ").Trim(); 
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str; 
        }
        
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
}