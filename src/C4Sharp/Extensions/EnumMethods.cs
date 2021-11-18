using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace C4Sharp.Extensions
{
    /// <summary>
    /// Enum extension methods
    /// </summary>
    internal static class EnumMethods
    {
        /// <summary>
        /// Get Description from Enum item
        /// </summary>
        /// <param name="genericEnum">Enum</param>
        /// <returns>Description of enum</returns>
        [ExcludeFromCodeCoverage]
        public static string GetDescription(this Enum genericEnum)
        {
            var genericEnumType = genericEnum.GetType();
            var memberInfo = genericEnumType.GetMember(genericEnum.ToString());

            if (memberInfo.Length <= 0)
            {
                return genericEnum.ToString();
            }

            var attribs = memberInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            
            return attribs.Any() 
                ? ((System.ComponentModel.DescriptionAttribute)attribs.ElementAt(0)).Description 
                : genericEnum.ToString();
        }
    }
}