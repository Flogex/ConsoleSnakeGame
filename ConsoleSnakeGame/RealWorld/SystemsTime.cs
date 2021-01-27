using System;
using System.Reactive.Linq;

namespace ConsoleSnakeGame.RealWorld
{
    internal static class SystemsTime
    {
        public static IObservable<long> Create() =>
            Observable.Interval(TimeSpan.FromMilliseconds(200));
    }
}
