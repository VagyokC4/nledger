using System.Collections.Generic;
using System.IO;

namespace NLedger.API.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     Get lines from string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetLines(this string value)
        {
            using var stringReader = new StringReader(value);
            while (stringReader.ReadLine() is { } line)
                yield return line;
        }
    }
}