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

        public void RenderNext(Position nextFoodPosition)
        {
            if (_lastFoodPosition.HasValue)
            {
                var lastFoodPosition = _lastFoodPosition.Value;
                _console.RemoveCharAt(lastFoodPosition.Y, lastFoodPosition.X);
            }

            _console.WriteAt('F', nextFoodPosition.Y, nextFoodPosition.X);

            _lastFoodPosition = nextFoodPosition;
        }
    }
}
