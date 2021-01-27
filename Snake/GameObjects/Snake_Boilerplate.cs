using System;
using System.Linq;
using System.Diagnostics;

namespace ConsoleSnakeGame.GameObjects
{
    [DebuggerDisplay("Snake (Head: {this.Head}, Length: {this.Length})")]
    public readonly partial struct Snake : IEquatable<Snake>
    {
        public override bool Equals(object? obj) =>
            obj is Snake snake && Equals(snake);

        public bool Equals(Snake other) =>
            this.Body.SequenceEqual(other.Body);

        public override int GetHashCode()
        {
            var hashCode = new HashCode();

            foreach (var part in _parts)
                hashCode.Add(part);

            return hashCode.ToHashCode();
        }

        public static bool operator ==(Snake left, Snake right) =>
            left.Equals(right);

        public static bool operator !=(Snake left, Snake right) =>
            !(left == right);
    }
}
