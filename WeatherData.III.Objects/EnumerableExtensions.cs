using System;
using System.Collections.Generic;
using System.Linq;

static internal class EnumerableExtensions
{
    public static IEnumerable<string> SkipUntil(this IEnumerable<string> lines, Func<string, bool> test)
    {
        return lines
            .SkipWhile(line => !test(line))
            .Skip(1);
    }
}