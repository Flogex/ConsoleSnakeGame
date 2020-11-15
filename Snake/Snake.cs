using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Snake
{
    public readonly struct Snake : IEquatable<Snake>
    {
        private readonly ImmutableList<Position> _parts;
        private readonly Position? _previousLastPartPosition;

        public Snake(int x, int y) : this(new Position(x, y)) { }

        public Snake(Position initialHead)
        {
            _parts = ImmutableList.Create(initialHead);
            _previousLastPartPosition = null;
        }

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
            var newHeadPosition = this.Head.AdjacentPosition(direction);

            var newBodyBuilder = _parts.ToBuilder();
            newBodyBuilder.RemoveAt(newBodyBuilder.Count - 1);
            newBodyBuilder.Insert(0, newHeadPosition);

            return new Snake(newBodyBuilder.ToImmutable(), _parts[^1]);
        }

        public Snake Eat()
        {
            if (!_previousLastPartPosition.HasValue)
                throw new InvalidOperationException("You have to move before you can eat");

            var newBody = _parts.Add(_previousLastPartPosition.Value);
            return new Snake(newBody, null);
        }

        public override bool Equals(object? obj) =>
            obj is Snake snake && Equals(snake);

        public bool Equals(Snake other) =>
            this.Body.SequenceEqual(other.Body);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var part in _parts)
                hashCode.Add(part);

            return hashCode.ToHashCode();
        }

        public static bool operator ==(Snake left, Snake right) =>
            left.Equals(right);

        public static bool operator !=(Snake left, Snake right) =>
            !(left == right);
    }
}
