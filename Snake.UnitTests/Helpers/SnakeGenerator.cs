using System;
using ConsoleSnakeGame.GameObjects;
using static ConsoleSnakeGame.GameObjects.Direction;

namespace ConsoleSnakeGame.UnitTests.Helpers
{
    internal static class SnakeGenerator
    {
        /// <summary>
        /// Creates a snake with parts at the given positions.
        /// </summary>
        /// <param name="positions">List starts with head</param>
        public static Snake CreateSnakeAtPositions(params Position[] positions)
        {
            var snake = new Snake(positions[^1]);

            for (var i = positions.Length - 1; i > 0; i--)
            {
                var direction = GetDirectionFromPositionVector(positions[i], positions[i - 1]);
                snake = snake.Move(direction).Eat();
            }

            return snake;
        }

        private static Direction GetDirectionFromPositionVector(Position start, Position end)
        {
            var vector = (end.X - start.X, end.Y - start.Y);

            return vector switch
            {
                ( < 0, 0) => Left,
                ( > 0, 0) => Right,
                (0, < 0) => Up,
                (0, > 0) => Down,
                _ => throw new ArgumentException("Unknown direction")
            };
        }
    }
}
