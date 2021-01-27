using ConsoleSnakeGame.Gameplay;

namespace ConsoleSnakeGame.Rendering
{
    public class StageRenderer
    {
        private readonly SnakeRenderer _snakeRenderer;
        private readonly FoodRenderer _foodRenderer;

        public StageRenderer(IConsole console)
        {
            _snakeRenderer = new SnakeRenderer(console);
            _foodRenderer = new FoodRenderer(console);
        }

        public void RenderNext(IStage stage)
        {
            // FoodRenderer must be called before SnakeRenderer because it
            // erased the position where the old food was. If the renderer
            // where called in a different order, the snake head would not
            // be rendered.
            // Theoretically, the FoodRenderer does not need to erase the
            // old food from the console because the snake head will appear
            // at this position. But I think it is easier to reason about
            // the classes when they are not that dependent.

            _foodRenderer.RenderNext(stage.CurrentFoodPosition);
            _snakeRenderer.RenderNext(stage.Snake);
        }
    }
}
