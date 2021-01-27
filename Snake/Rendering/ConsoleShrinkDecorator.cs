using System;

namespace ConsoleSnakeGame.Rendering
{
    public class ConsoleShrinkDecorator : IConsole
    {
        private readonly IConsole _inner;

        public ConsoleShrinkDecorator(int width, int height, IConsole inner)
        {
            _inner = inner;

            const string message = "Shrunk console must be smaller than decorated console.";
            this.Width = width <= _inner.Width
                ? width
                : throw new ArgumentException(message, nameof(width));
            this.Height = height <= _inner.Height
                ? height
                : throw new ArgumentException(message, nameof(height));
        }

        public int Width { get; }

        public int Height { get; }

        public void RemoveCharAt(CursorPosition position)
        {
            ThrowIfOutOfBounds(position);
            _inner.RemoveCharAt(position);
        }

        public void WriteAt(char value, CursorPosition position)
        {
            ThrowIfOutOfBounds(position);
            _inner.WriteAt(value, position);
        }

        public void WriteAt(string value, CursorPosition position)
        {
            ThrowIfOutOfBounds(position);
            _inner.WriteAt(value, position);
        }

        private void ThrowIfOutOfBounds(CursorPosition position)
        {
            if (position.Row >= this.Height || position.Column >= this.Width)
            {
                throw new ArgumentException(
                    $"Cannot write or remove char outside the console.",
                    nameof(position));
            }
        }
    }
}
