namespace ConsoleSnakeGame.Rendering
{
    public class ConsoleFrameDecorator : IConsole
    {
        public const int BorderWidth = 1;

        private const char _frameSymbol = '#';
        private readonly IConsole _inner;

        public ConsoleFrameDecorator(IConsole consoleToEnframe)
        {
            _inner = consoleToEnframe;
            DrawFrame();
        }

        private void DrawFrame()
        {
            var horizontalFrame = new string(_frameSymbol, _inner.Width);
            _inner.WriteAt(horizontalFrame, 0, 0);
            _inner.WriteAt(horizontalFrame, _inner.Height - 1, 0);

            for (var row = 1; row <= _inner.Height - 2; row++)
            {
                _inner.WriteAt(_frameSymbol, row, 0);
                _inner.WriteAt(_frameSymbol, row, _inner.Width - 1);
            }
        }

        public int Width => _inner.Width - 2 * BorderWidth;

        public int Height => _inner.Height - 2 * BorderWidth;

        public void RemoveCharAt(CursorPosition position) =>
            _inner.RemoveCharAt(TranslateCursorPosition(position));

        public void WriteAt(char value, CursorPosition position) =>
            _inner.WriteAt(value, TranslateCursorPosition(position));

        public void WriteAt(string value, CursorPosition position) =>
            _inner.WriteAt(value, TranslateCursorPosition(position));

        private static CursorPosition TranslateCursorPosition(CursorPosition position) =>
            new CursorPosition(position.Row + BorderWidth, position.Column + BorderWidth);
    }
}
