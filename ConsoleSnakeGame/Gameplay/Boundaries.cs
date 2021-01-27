using System;
using ConsoleSnakeGame.GameObjects;

namespace ConsoleSnakeGame.Gameplay
{
    public readonly struct Boundaries : IEquatable<Boundaries>
    {
        public Boundaries(int height, int width)
        {
            MinX = 0;
            MaxX = width - 1;
            MinY = 0;
            MaxY = height - 1;
        }

        public int MinX { get; }

        public int MaxX { get; }

        public int MinY { get; }

        public int MaxY { get; }

        public bool IsOutOfBounds(Position position)
        {
            return
                position.X < MinX ||
                position.X > MaxX ||
                position.Y < MinY ||
                position.Y > MaxY;
        }

        public override bool Equals(object? obj) =>
            obj is Boundaries boundaries && Equals(boundaries);

        public bool Equals(Boundaries other) =>
            MinX == other.MinX &&
            MaxX == other.MaxX &&
            MinY == other.MinY &&
            MaxY == other.MaxY;

        public override int GetHashCode() =>
            HashCode.Combine(MinX, MaxX, MinY, MaxY);

        public static bool operator ==(Boundaries left, Boundaries right) =>
            left.Equals(right);
        public static bool operator !=(Boundaries left, Boundaries right) =>
            !(left == right);
    }
}
