using System;
using static Snake.Direction;

namespace Snake
{
    public static class PositionExtensions
    {
        public static Position AdjacentPosition(this Position pos, Direction direction)
        {
            return direction switch
            {
                Left  => (pos.X - 1, pos.Y),
                Right => (pos.X + 1, pos.Y),
                Up    => (pos.X, pos.Y - 1),
                Down  => (pos.X, pos.Y + 1),
                _ => throw new NotSupportedException("Unknown direction")
            };
        }
    }
}
