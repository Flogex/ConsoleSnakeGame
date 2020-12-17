using System;
using System.Reactive.Linq;

namespace Snake.RealWorld
{
    public static class UserDirections
    {
        public static IObservable<Direction> Create()
        {
            return Observable.Empty<Direction>();
        }
    }
}
