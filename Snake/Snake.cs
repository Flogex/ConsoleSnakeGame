using System;
using System.Collections.Generic;
using static Snake.Direction;

namespace Snake
{
    public class Snake
    {
        private Position? _previousLastPartPosition;

        public Snake(int x, int y) : this(new Position(x, y)) { }

        public Snake(Position initialHead)
        {
            this.Body = new List<Position> { initialHead };
        }

        public Position Head => this.Body[0];

        public List<Position> Body { get; }

        public int Length => this.Body.Count;

        public void Move(Direction direction)
        {
            _previousLastPartPosition = this.Body[^1];

            var newHeadPosition = direction switch
            {
                Left => this.Head.WithX(this.Head.X - 1),
                Right => this.Head.WithX(this.Head.X + 1),
                Up => this.Head.WithY(this.Head.Y - 1),
                Down => this.Head.WithY(this.Head.Y + 1),
                _ => throw new NotSupportedException("Unknown direction")
            };

            this.Body.RemoveAt(this.Body.Count - 1);
            this.Body.Insert(0, newHeadPosition);
        }

        public void Eat()
        {
            if (!_previousLastPartPosition.HasValue)
                throw new InvalidOperationException("You have to move before you can eat");

            this.Body.Add(_previousLastPartPosition.Value);
        }
    }
}
