using System;
using System.Reactive.Linq;
using static System.ConsoleKey;

namespace Snake.RealWorld
{
    public static class UserDirections
    {
        // https://stackoverflow.com/a/10678328
        public static IObservable<Direction> Create() =>
            Observable
                .Defer(CreateReadKeyObservable)
                .Repeat()
                .Select(ConsoleKeyToDirection)
                .Where(d => d.HasValue)
                .Select(d => d!.Value);

        private static IObservable<ConsoleKeyInfo> CreateReadKeyObservable() =>
            Observable.Start(() => Console.ReadKey(true));

        private static Direction? ConsoleKeyToDirection(ConsoleKeyInfo keyInfo) =>
            keyInfo.Key switch
            {
                LeftArrow => Direction.Left,
                RightArrow => Direction.Right,
                UpArrow => Direction.Up,
                DownArrow => Direction.Down,
                _ => null
            };
    }
}
