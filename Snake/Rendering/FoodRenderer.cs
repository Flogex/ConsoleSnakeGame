using Snake.GameObjects;

namespace Snake.Rendering
{
    internal class FoodRenderer
    {
        private const char _foodSymbol = 'F';

        private readonly IConsole _console;
        private Position? _lastFoodPosition;

        public FoodRenderer(IConsole console)
        {
            _console = console;
        }

        public void RenderNext(Position nextFoodPosition)
        {
            if (_lastFoodPosition == nextFoodPosition)
                return;

            if (_lastFoodPosition.HasValue)
            {
                var lastFoodPosition = _lastFoodPosition.Value;
                _console.RemoveCharAt(lastFoodPosition.Y, lastFoodPosition.X);
            }

            _console.WriteAt(_foodSymbol, nextFoodPosition.Y, nextFoodPosition.X);

            _lastFoodPosition = nextFoodPosition;
        }
    }
}
