using ConsoleSnakeGame.GameObjects;
using System;
using System.Linq;

namespace ConsoleSnakeGame.Gameplay
{
    internal static class Referee
    {
        public static bool HasPlayerLost(Snake snake, Boundaries boundaries) =>
            HasSnakeLeftStage(snake, boundaries) || HasSnakeEatenItself(snake);

        private static bool HasSnakeLeftStage(Snake snake, Boundaries boundaries) =>
            boundaries.IsOutOfBounds(snake.Head);

        private static bool HasSnakeEatenItself(Snake snake) =>
            snake.Body.Skip(1).Contains(snake.Head);
    }
}
