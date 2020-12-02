using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Snake.GameObjects;

namespace Snake
{
    public readonly partial struct Snake
    {
        private readonly ImmutableList<Position> _parts;
        private readonly Position? _previousLastPartPosition;

        public Snake(int x, int y) : this(new Position(x, y)) { }

        public Snake(Position initialHead) : this(ImmutableList.Create(initialHead), null) { }

        private Snake(ImmutableList<Position> body, Position? previousLastPartPosition)
        {
            _parts = body;
            _previousLastPartPosition = previousLastPartPosition;
        }

        public Position Head => _parts[0];

        public IReadOnlyList<Position> Body => _parts;

        public int Length => _parts.Count;

        public Snake Move(Direction direction)
        {
            var newBodyBuilder = _parts.ToBuilder();

            var newHeadPosition = this.Head.AdjacentPosition(direction);
            newBodyBuilder.Insert(0, newHeadPosition);

            newBodyBuilder.RemoveAt(newBodyBuilder.Count - 1);

            return new Snake(newBodyBuilder.ToImmutable(), _parts[^1]);
        }

        public Snake Eat()
        {
            if (!_previousLastPartPosition.HasValue)
                throw new InvalidOperationException("You have to move before you can eat");

            var newBody = _parts.Add(_previousLastPartPosition.Value);
            return new Snake(newBody, null);
        }
    }
}
