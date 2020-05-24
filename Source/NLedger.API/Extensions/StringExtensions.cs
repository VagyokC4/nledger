using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NLedger.API.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Normalize string removing ANSI characters.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Normalize(this string value)
        {
            return value
                .Replace("[0m", "")
                .Replace("[1m", "")
                .Replace("[31m", "")
                .Replace("[34m", "");
        }

        /// <summary>
        ///     Split string into lines
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<string> ToLines(this string value)
        {
            return value.GetLines().ToList();
        }

        /// <summary>
        ///     Get lines from string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetLines(this string value)
        {
            using (var stringReader = new StringReader(value))
            {
                while (stringReader.ReadLine() is string line)
                    yield return line;
            }
        }
    }
}