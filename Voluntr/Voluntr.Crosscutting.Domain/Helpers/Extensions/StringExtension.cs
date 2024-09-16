using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Voluntr.Crosscutting.Domain.Helpers.Extensions
{
    public static class StringExtension
    {
        public static string FormatToHiddenEmail(this string email)
        {
            string pattern = @"(?<=[\w]{1})[\w-\._\+%]*(?=[\w]{1}@)";
            string result = Regex.Replace(email, pattern, m => new string('*', m.Length));

            return result;
        }

        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} não pode ser vazio", nameof(input)),
                _ => input.First().ToString().ToUpper() + input[1..],
            };
        }

        public static string ToLowerFormat(this string text)
        {
            if (!string.IsNullOrEmpty(text))
                return text.ToLower();

            return null;
        }

        public static string Capitalize(this string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.TrimStart()
                    .TrimEnd()
                    .ToLower();

                TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;

                string[] words = text.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Length > 3 || i == 0)
                        words[i] = textInfo.ToTitleCase(words[i]);
                }

                return string.Join(" ", words);
            }

            return null;
        }

        public static bool IsJson(this string source)
        {
            if (source == null)
                return false;

            try
            {
                JsonDocument.Parse(source);
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
