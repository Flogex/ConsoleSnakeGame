using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Rendering
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
            _snakeRenderer.RenderNext(stage.Snake);
            _foodRenderer.RenderNext(stage.CurrentFoodPosition);
        }
    }
}
