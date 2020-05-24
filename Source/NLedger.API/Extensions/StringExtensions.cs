using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NLedger.API.Extensions
{
    public static class StringExtensions
    {
        public static string Normalize(this string value)
        {
            return value
                .Replace("[0m", "")
                .Replace("[1m", "")
                .Replace("[31m", "")
                .Replace("[34m", "");
        }

        public static List<string> ToLines(this string value)
        {
            return value.Lines().ToList();
        }

        public static IEnumerable<string> Lines(this string value)
        {
            using (var stringReader = new StringReader(value))
            {
                while (stringReader.ReadLine() is string line)
                    yield return line;
            }
        }
    }
}