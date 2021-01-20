using System.Collections.Generic;
using Xunit;

namespace Snake.UnitTests.Helpers
{
    public static class TheoryDataExtensions
    {
        public static TheoryData<T1, T2> Fill<T1, T2>(
            this TheoryData<T1, T2> theoryData,
            IEnumerable<(T1, T2)> input)
        {
            foreach (var (first, second) in input)
                theoryData.Add(first, second);

            return theoryData;
        }
    }
}
