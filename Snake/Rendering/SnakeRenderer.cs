namespace Snake.Rendering
{
    internal class SnakeRenderer
    {
        private readonly IConsole _console;
        private Snake? _currentSnake;

        public SnakeRenderer(IConsole console)
        {
            _console = console;
        }

        public void RenderNext(Snake nextSnake)
        {
            // Assumption: The next snake rendered has only moved one field.

            if (_currentSnake.HasValue)
                RenderSnakeDiff(_currentSnake.Value, nextSnake);
            else
                RenderFullSnake(nextSnake);

            _currentSnake = nextSnake;
        }

        private void RenderSnakeDiff(Snake currentSnake, Snake nextSnake)
        {
            var partToRemove = currentSnake.Body[^1];
            _console.RemoveCharAt(partToRemove.Y, partToRemove.X);

            var partToAdd = nextSnake.Head;
            _console.WriteAt('X', partToAdd.Y, partToAdd.X);
        }

        private void RenderFullSnake(Snake snake)
        {
            foreach (var part in snake.Body)
                _console.WriteAt('X', part.Y, part.X);
        }
    }
}
