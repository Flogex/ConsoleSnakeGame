namespace Snake.Rendering
{
    internal class SnakeRenderer
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
                _console.WriteAt('X', part.Y, part.X);
        }

        private void ClearConsole()
        {
            for (var row = 0; row < _console.Height; row++)
            {
                for (var column = 0; column < _console.Width; column++)
                {
                    _console.RemoveCharAt(row, column);
                }
            }
        }
    }
}
