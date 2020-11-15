using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using static Snake.Direction;

namespace Snake
{
    public readonly struct Snake
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
            var newHeadPosition = direction switch
            {
                Left => this.Head.WithX(this.Head.X - 1),
                Right => this.Head.WithX(this.Head.X + 1),
                Up => this.Head.WithY(this.Head.Y - 1),
                Down => this.Head.WithY(this.Head.Y + 1),
                _ => throw new NotSupportedException("Unknown direction")
            };

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
    }
}
