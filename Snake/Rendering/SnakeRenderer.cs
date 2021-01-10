namespace Snake.Rendering
{
    internal class SnakeRenderer
    {
        private const char _partSymbol = 'X';

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
            // If the length stays the same, the snake has only moved one field.
            // All parts move to the position of their predecessor and the head
            // moves one field forward. Therefore, it is enough to remove the last
            // part of the current snake and render the new head.
            // If the length is not the same, the snake has grown. The whole snake
            // moves one field forward, but at the same time one new part is added
            // at the end of the tail. Hence there is no need to erase the last
            // part of the current snake.

            if (currentSnake.Length == nextSnake.Length)
            {
                var partToRemove = currentSnake.Body[^1];
                _console.RemoveCharAt(partToRemove.Y, partToRemove.X);
            }

            var partToAdd = nextSnake.Head;
            _console.WriteAt(_partSymbol, partToAdd.Y, partToAdd.X);
        }

        private void RenderFullSnake(Snake snake)
        {
            foreach (var part in snake.Body)
                _console.WriteAt(_partSymbol, part.Y, part.X);
        }
    }
}
