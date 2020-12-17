using System;
using System.Linq;
using Snake.GameObjects;

namespace Snake.RealWorld
{
    public class RandomFoodPositioningService : IFoodPositioningService
    {
        private readonly Random _random;

        public RandomFoodPositioningService()
        {
            _random = new Random();
        }

        public Position GetNextPosition(Boundaries boundaries, Snake snake)
        {
            return (3, 7);

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
