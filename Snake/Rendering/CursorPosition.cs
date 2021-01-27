using System;

namespace ConsoleSnakeGame.Rendering
{
    public readonly struct CursorPosition : IEquatable<CursorPosition>
    {
        /// <summary>
        /// Creates a new CursorPosition struct.
        /// </summary>
        /// <param name="row">The zero-based column position of the cursor.</param>
        /// <param name="column">The zero-based row position of the cursor.</param>
        public CursorPosition(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row { get; }

        public int Column { get; }

        public override bool Equals(object? obj) =>
            obj is CursorPosition position && Equals(position);

        public bool Equals(CursorPosition other) =>
            this.Row == other.Row && this.Column == other.Column;

        public override int GetHashCode() =>
            HashCode.Combine(this.Row, this.Column);

        public static bool operator ==(CursorPosition left, CursorPosition right) =>
            left.Equals(right);

        public static bool operator !=(CursorPosition left, CursorPosition right) =>
            !(left == right);

        public void Deconstruct(out int row, out int column)
        {
            row = this.Row;
            column = this.Column;
        }
    }
}
