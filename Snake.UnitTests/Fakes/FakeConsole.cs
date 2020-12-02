using System.Linq;
using Snake.GameObjects;
using Snake.Rendering;

namespace Snake.UnitTests.Fakes
{
    internal class FakeConsole : IConsole
    {
        private (int Row, int Column) _cursorPosition = (0, 0);

        public FakeConsole(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Output = new char[this.Height, this.Width];
        }

        public int Width { get; }

        public int Height { get; }

        public char[,] Output { get; }

        public void SetCursorPosition(int row, int column) =>
            _cursorPosition = (row, column);

        public void Write(char value)
        {
            this.Output[_cursorPosition.Row, _cursorPosition.Column] = value;
            MoveCursorToFollowingPosition();
        }

        private void MoveCursorToFollowingPosition()
        {
            var currentColumn = _cursorPosition.Column;
            var currentRow = _cursorPosition.Row;

            var nextColumn = (currentColumn + 1) % this.Width;
            var nextRow = nextColumn <= currentColumn ? currentRow + 1 : currentRow;

            _cursorPosition = (nextRow, nextColumn);
        }

        public void Write(string value)
        {
            foreach (var @char in value)
                Write(@char);
        }

        public void RemoveCurrentChar()
        {
            this.Output[_cursorPosition.Row, _cursorPosition.Column] = default;
            MoveCursorToPrecedingPosition();
        }

        private void MoveCursorToPrecedingPosition()
        {
            var currentColumn = _cursorPosition.Column;
            var currentRow = _cursorPosition.Row;

            var nextColumn = (currentColumn - 1 + this.Width) % this.Width;
            var nextRow = nextColumn >= currentColumn ? currentRow - 1 : currentRow;

            _cursorPosition = (nextRow, nextColumn);
        }

        public bool PositionsSet(params Position[] positions) =>
            positions.All(p => this.Output[p.Y, p.X] != default);
    }

    //internal readonly struct TextSpan
    //{
    //    public TextSpan(Index start, string text)
    //    {
    //        this.Range = new Range(start, new Index(start.Value + text.Length));
    //        this.Text = text;
    //    }

    //    public Range Range { get; }

    //    public string Text { get; }

    //    public bool Overlaps(TextSpan other) =>
    //        this.Range.Overlaps(other.Range);

    //    public TextSpan CombineOverlapping(TextSpan other)
    //    {
    //        if (!Overlaps(other))
    //            throw new ArgumentException(nameof(other));

    //        var textBuilder = new StringBuilder();

    //    }

    //}

    //internal static class RangeExtensions
    //{
    //    public static bool Overlaps(this Range @this, Range other) =>
    //        OverlapsIntern(@this, other) || OverlapsIntern(other, @this);

    //    private static bool OverlapsIntern(Range first, Range second) =>
    //        first.Start.Value <= second.Start.Value &&
    //        first.End.Value >= second.Start.Value;
    //}
}
