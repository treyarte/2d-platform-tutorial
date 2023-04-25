using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Extensions
{
    public static class EnumExtensions
    {
        /// <summary>Gets the <see cref="DescriptionAttribute"/> of the value, otherwise returns the string value of the value</summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

            DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return descriptionAttribute == null ? value.ToString() : descriptionAttribute.Description;
        }
			
        /// <summary>
        /// Get the value of a custom attribute on an enum
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First() // We want the first because we could have multiple of the same custom attribute
                .GetCustomAttribute<TAttribute>();
        }
    }
}