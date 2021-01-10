using System.Collections.Generic;
using System.Linq;
using Snake.GameObjects;
using Snake.Rendering;

namespace Snake.UnitTests.Fakes
{
    internal class FakeConsole : IConsole
    {
        public FakeConsole(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Output = new char[this.Height, this.Width];
        }

        public int Width { get; }

        public int Height { get; }

        public char[,] Output { get; }

        public void WriteAt(char value, CursorPosition position)
        {
            this.Output[position.Row, position.Column] = value;
        }

        public void WriteAt(string value, CursorPosition position)
        {
            var nextPosition = position;

            foreach (var @char in value)
            {
                WriteAt(@char, nextPosition);
                nextPosition = FollowingCursorPosition(nextPosition);
            }
        }

        public void RemoveCharAt(CursorPosition position)
        {
            this.Output[position.Row, position.Column] = default;
        }

        private CursorPosition FollowingCursorPosition(CursorPosition position)
        {
            var (currentRow, currentColumn) = position;

            var nextColumn = (currentColumn + 1) % this.Width;
            var nextRow = nextColumn <= currentColumn ? currentRow + 1 : currentRow;

            return new CursorPosition(nextRow, nextColumn);
        }

        private CursorPosition PrecedingCursorPosition(CursorPosition position)
        {
            var (currentRow, currentColumn) = position;

            var nextColumn = (currentColumn - 1 + this.Width) % this.Width;
            var nextRow = nextColumn >= currentColumn ? currentRow - 1 : currentRow;

            return new CursorPosition(nextRow, nextColumn);
        }

        public bool ArePositionsSet(params Position[] positions) =>
            positions.All(p => this.Output[p.Y, p.X] != default);

        public IEnumerable<Position> GetSetPositions()
        {
            for (var row = 0; row < this.Output.GetLength(0); row++)
            {
                for (var column = 0; column < this.Output.GetLength(1); column++)
                {
                    if (this.Output[row, column] != default)
                        yield return new Position(column, row);
                }
            }
        }
    }
}
