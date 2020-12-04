using Snake.GameObjects;

namespace Snake.Rendering
{
    internal class FoodRenderer
    {
        private readonly IConsole _console;
        private Position? _lastFoodPosition;

        public FoodRenderer(IConsole console)
        {
            _console = console;
        }

        public void RenderNext(Position foodPosition)
        {
            if (_lastFoodPosition.HasValue)
            {
                var lastFoodPosition = _lastFoodPosition.Value;
                _console.SetCursorPosition(lastFoodPosition.Y, lastFoodPosition.X);
                _console.RemoveCurrentChar();
            }

            _console.SetCursorPosition(foodPosition.Y, foodPosition.X);
            _console.Write('F');

            _lastFoodPosition = foodPosition;
        }
    }
}
