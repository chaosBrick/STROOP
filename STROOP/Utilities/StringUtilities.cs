using System;
using System.Collections.Generic;
using System.Reflection;

namespace STROOP.Utilities
{
    /// <summary>
    /// Denotes that a static string variable's value shall be initialized with its field name when <see cref="StringUtilities.InitializeDeclaredStrings(Type)"/> is called on its declaring type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class DeclaredStringAttribute : Attribute { }

    public static class StringUtilities
    {
        public static void InitializeDeclaredStrings(Type t)
        {
            foreach (var field in t.GetFields(BindingFlags.Public | BindingFlags.Static))
                if (field.FieldType == typeof(string))
                    field.SetValue(null, field.Name);
        }

        public static string Cap(string stringValue, int length)
        {
            if (stringValue == null) return stringValue;
            if (stringValue.Length <= length) return stringValue;
            return stringValue.Substring(0, length);
        }

        public static string ExactLength(string stringValue, int length, bool leftAppend, char appendChar)
        {
            if (stringValue == null) return stringValue;
            if (stringValue.Length < length)
            {
                return leftAppend
                    ? stringValue.PadLeft(length, appendChar)
                    : stringValue.PadRight(length, appendChar);
            }
            if (stringValue.Length > length)
            {
                return leftAppend
                  ? stringValue.Substring(stringValue.Length - length)
                  : stringValue.Substring(0, length);
            }
            return stringValue;
        }

        public static string FormatIntegerWithSign(int num)
        {
            return (num > 0 ? "+" : "") + num;
        }

        public static string Capitalize(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return s.Substring(0, 1).ToUpper() + s.Substring(1);
        }

        public static string Concat<T>(IEnumerable<T> ts, Func<T, string> toString)
        {
            List<string> strings = new List<string>();
            foreach (var t in ts)
                strings.Add(toString(t));
            return string.Concat(strings);
        }

        static System.Text.RegularExpressions.Regex needsJsonStringEscapeRegex = new System.Text.RegularExpressions.Regex("^[-]?(([0-9]+)|(([0-9]+)\\.([0-9]+)))$");
        public static string MakeJsonValue(string input)
        {
            input = input.Trim(' ', '"');
            if (!needsJsonStringEscapeRegex.IsMatch(input))
                return $"\"{input}\"";
            return input;
        }
    }
}
