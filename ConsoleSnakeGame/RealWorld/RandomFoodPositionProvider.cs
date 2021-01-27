﻿using System;
using System.Linq;
using ConsoleSnakeGame.Gameplay;
using ConsoleSnakeGame.GameObjects;
using ConsoleSnakeGame.Ports;

namespace ConsoleSnakeGame.RealWorld
{
    internal class RandomFoodPositionProvider : IFoodPositionProvider
    {
        private readonly Random _random;

        public RandomFoodPositionProvider()
        {
            _random = new Random();
        }

        public Position GetNextPosition(Boundaries boundaries, Snake snake)
        {
            if (snake.Length == (boundaries.MaxX + 1) * (boundaries.MaxY + 1))
                throw new InvalidOperationException("There is no place for new food.");

            (int, int) position;

            do
            {
                position = GetRandomPosition(boundaries);
            }
            while (Collides(position, snake));

            return position;
        }

        private (int x, int y) GetRandomPosition(Boundaries boundaries)
        {
            var x = _random.Next(boundaries.MinX, boundaries.MaxX);
            var y = _random.Next(boundaries.MinY, boundaries.MaxY);
            return (x, y);
        }

        private static bool Collides((int X, int Y) position, Snake snake) =>
            snake.Body.Contains((position.X, position.Y));
    }
}
