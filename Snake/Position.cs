﻿using System;

namespace Snake
{
    public readonly struct Position : IEquatable<Position>
    {
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public Position WithX(int newX) =>
            new Position(newX, this.Y);

        public Position WithY(int newY) =>
            new Position(this.X, newY);

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
    }
}
