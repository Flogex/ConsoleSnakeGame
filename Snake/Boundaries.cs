using System;

namespace Snake
{
    public readonly struct Boundaries : IEquatable<Boundaries>
    {
        public Boundaries(int size)
        {
            this.MinX = 0;
            this.MaxX = size - 1;
            this.MinY = 0;
            this.MaxY = size - 1;
        }

        public int MinX { get; }

        public int MaxX { get; }

        public int MinY { get; }

        public int MaxY { get; }

        public bool IsOutOfBounds(Position position)
        {
            return
                position.X < this.MinX ||
                position.X > this.MaxX ||
                position.Y < this.MinY ||
                position.Y > this.MaxY;
        }

        public override bool Equals(object? obj) =>
            obj is Boundaries boundaries && Equals(boundaries);

        public bool Equals(Boundaries other) =>
            this.MinX == other.MinX &&
            this.MaxX == other.MaxX &&
            this.MinY == other.MinY &&
            this.MaxY == other.MaxY;

        public override int GetHashCode() =>
            HashCode.Combine(this.MinX, this.MaxX, this.MinY, this.MaxY);

        public static bool operator ==(Boundaries left, Boundaries right) =>
            left.Equals(right);
        public static bool operator !=(Boundaries left, Boundaries right) =>
            !(left == right);
    }
}
