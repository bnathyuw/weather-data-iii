using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherData.III.Objects.Domain
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<string> SkipUntil(this IEnumerable<string> lines, Func<string, bool> test)
        {
            return lines
                .SkipWhile(line => !test(line))
                .Skip(1);
        }
    }
}