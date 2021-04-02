using System;
using System.Linq;

namespace C4Sharp.Extensions
{
    internal static class EnumMethods
    {
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