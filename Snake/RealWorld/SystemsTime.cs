using System;
using System.Reactive.Linq;

namespace Snake.RealWorld
{
    public static class SystemsTime
    {
        public static IObservable<long> Create() =>
            Observable.Interval(TimeSpan.FromSeconds(1));
    }
}
