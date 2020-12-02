namespace Snake.Rendering
{
    public class SnakeRenderer
    {
        private readonly IConsole _console;

        public SnakeRenderer(IConsole console)
        {
            _console = console;
        }

        public void RenderNext(Snake snake)
        {
            ClearConsole();

            foreach (var part in snake.Body)
            {
                _console.SetCursorPosition(part.Y, part.X);
                _console.Write('X');
            }
        }

        private void ClearConsole()
        {
            _console.SetCursorPosition(_console.Height - 1, _console.Width - 1);

            for (var row = 0; row < _console.Height; row++)
            {
                for (var column = 0; column < _console.Width; column++)
                {
                    _console.RemoveCurrentChar();
                }
            }
        }
    }
}
