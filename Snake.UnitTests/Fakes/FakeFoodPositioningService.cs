using System;
using System.Collections.Generic;
using System.Linq;

namespace Snake.UnitTests.Fakes
{
    internal class FakeFoodPositioningService : IFoodPositioningService
    {
        private readonly IEnumerator<Position> _positionsEnumerator;

        public FakeFoodPositioningService(params Position[] positions) : this(positions.AsEnumerable()) { }

        public FakeFoodPositioningService(IEnumerable<Position> positions)
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
