using System;
using System.Diagnostics;

namespace Snake
{
    [DebuggerDisplay("({this.X}, {this.Y})")]
    public readonly struct Position : IEquatable<Position>
    {
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public override bool Equals(object? obj) =>
            obj is Position position && Equals(position);

        public bool Equals(Position other) =>
            this.X == other.X && this.Y == other.Y;

        public override int GetHashCode() =>
            HashCode.Combine(this.X, this.Y);

        public static bool operator ==(Position left, Position right) =>
            left.Equals(right);

        public static bool operator !=(Position left, Position right) =>
            !(left == right);

        public static implicit operator Position((int X, int Y) tuple) =>
            new Position(tuple.X, tuple.Y);
    }
}
