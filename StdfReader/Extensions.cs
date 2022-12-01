using System;
using System.Text.RegularExpressions;

namespace StdfReader
{
    public static class Extensions
    {
        /// <summary>
        /// Get the default value of a <see cref="Type"/>
        /// </summary>
        /// <param name="source">The <see cref="Type"/> of which get the default value</param>
        /// <returns>The default value</returns>
        public static object GetDefaultValue(this Type source)
        {
            object value = null;

            if (source.IsValueType)
                value = Activator.CreateInstance(source);

            return value;
        }

        /// <summary>
        /// Convert a pascal case <see cref="string"/> to an sentence string (i.e. PascalCase -> Pascal case)
        /// </summary>
        /// <param name="str">The <see cref="string"/> to convert</param>
        /// <returns>The converted <see cref="string"/></returns>
        public static string ToSentenceCase(this string str)
            => Regex.Replace(str, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
    }
}
