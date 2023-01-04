using System;
using System.Linq;
using System.Text.RegularExpressions;
// ReSharper disable UnusedMember.Global

namespace Wayway.Engine
{
    public static class StringExtension
    {
        /// <summary>
        /// Changing String to CamelCase
        /// </summary>
        public static string ToCamelCase(this string original)
        {
            if (string.IsNullOrEmpty(original) || char.IsLower(original, 0))
            {
                return original;
            }

            return char.ToLowerInvariant(ToPascalCase(original)[0]) + original[1..];
        }

        /// <summary>
        /// Changing String to PascalCase
        /// </summary>
        public static string ToPascalCase(this string original)
        {
            var invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
            var whiteSpace = new Regex(@"(?<=\s)");
            var startsWithLowerCaseChar = new Regex("^[a-z]");
            var firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
            var lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
            var upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

            // replace white spaces with underscore, then replace all invalid chars with empty string
            var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(original, "_"), string.Empty)
                .Split(new [] { '_' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))
                .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))
                .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))
                .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));
            
            var result = string.Concat(pascalCase);

            return result.Length <= 2
                ? result.ToUpper()
                : result;
        }
        
        /// <summary>
        /// Converts string to enum object
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="original">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string original)
            where T : struct
        {
            return (T) Enum.Parse(typeof (T), original, true);
        }
        
        public static bool IsNumeric(this string original)
        {
            return long.TryParse(original, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out _);
        }

        public static bool NotContains(this string original, char value)
        {
            return !original.Contains(value);
        }

        /// <summary>
        /// Add strings on array like CollectionType. Not obsolete but less recommendation...
        /// </summary>
        /// <param name="original">original array</param>
        /// <param name="item">new string</param>
        public static void Add(this string[] original, string item)
        {
            var newSize = original.IsNullOrEmpty() ? 1 : original.Length + 1;
            
            Array.Resize(ref original, newSize);
            original[^1] = item;
        }
    }
}
