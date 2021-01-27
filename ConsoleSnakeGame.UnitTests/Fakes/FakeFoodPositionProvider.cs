using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleSnakeGame.Gameplay;
using ConsoleSnakeGame.GameObjects;
using ConsoleSnakeGame.Ports;

namespace ConsoleSnakeGame.UnitTests.Fakes
{
    internal class FakeFoodPositionProvider : IFoodPositionProvider
    {
        private readonly IEnumerator<Position> _positionsEnumerator;

        public FakeFoodPositionProvider(params Position[] positions) : this(positions.AsEnumerable()) { }

        public FakeFoodPositionProvider(IEnumerable<Position> positions)
        {
            _positionsEnumerator = positions.GetEnumerator();
        }

        public Position GetNextPosition(Boundaries boundaries, Snake snake)
        {
            return _positionsEnumerator.MoveNext()
                ? _positionsEnumerator.Current
                : throw new InvalidOperationException("You have to pass more positions.");
        }
    }
}
