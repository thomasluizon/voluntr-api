﻿using System.ComponentModel;

namespace Voluntr.Crosscutting.Domain.Helpers.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum @enum)
        {
            var enumeratorItems = @enum.ToString().Split(',');
            var description = new string[enumeratorItems.Length];

            for (var i = 0; i < enumeratorItems.Length; i++)
            {
                var fieldInfo = @enum.GetType().GetField(enumeratorItems[i]?.Trim());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                description[i] = attributes.Length > 0 ? attributes[0].Description : enumeratorItems[i]?.Trim();
            }

            return string.Join(", ", description);
        }

        public static T GetEnumerator<T>(this string description)
        {
            if (description == null)
                return default;

            description = description.ToUpper();

            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description.ToUpper() == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.ToUpper() == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new InvalidCastException(string.Format("Enumerator {0} with description {1} not found", type, description));
        }
    }
}
