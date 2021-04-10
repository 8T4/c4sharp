using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace C4Sharp.Extensions
{
    internal static class StringMethods
    {
        internal static string AddSpaces(this string value, int size)
        {
            return value.PadLeft(size, ' ');
        }
        
        internal static string GenerateSlug(this string phrase) 
        { 
            var str = phrase.RemoveAccent().ToLower(CultureInfo.InvariantCulture); 
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
        
        public static string[] ToParagraph(this string phrase, int lineSize)
        {
            var tokens = phrase.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var result = string.Empty;
            var index = 0;
            var vector = new string[3];

            foreach (var token in tokens)
            {
                if (index >= 3)
                {
                    break;
                }

                var current = string.Join(' ', result, token);

                if (current.Length < lineSize)
                {
                    result = current;
                    vector[index] = current;
                    continue;
                }

                result = string.Empty;
                vector[index] = current;
                index++;
            }

            return vector;
        }        
    }
}