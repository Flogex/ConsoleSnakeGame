using System.Collections.Generic;
using System.Linq;

namespace ConsoleSnakeGame.UnitTests.Helpers
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<(T First, T Second)> CombineDistinctPairs<T>(this IEnumerable<T> source)
        {
            return source.SelectMany((first, index) =>
                source.Skip(index + 1).Select(second => (first, second)));
        }
    }
}
