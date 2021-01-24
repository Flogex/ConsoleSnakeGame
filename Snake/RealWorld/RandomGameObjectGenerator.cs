using Snake.GameObjects;
using System;

namespace Snake.RealWorld
{
    public static class RandomGameObjectGenerator
    {
        private static readonly Random _random = new Random();

        public static Position GetPosition(Boundaries boundaries)
        {
            var x = _random.Next(boundaries.MinX + 2, boundaries.MaxX - 1);
            var y = _random.Next(boundaries.MinY + 2, boundaries.MaxY - 1);
            return (x, y);
        }

        public static Direction GetDirection()
        {
            var randomValue = _random.Next(0, 4);
            return (Direction)randomValue;
        }
    }
}
