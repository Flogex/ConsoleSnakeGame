using System;
using System.Threading.Tasks;
using Snake.RealWorld;
using Snake.Rendering;

namespace Snake
{
    class Program
    {
        static async Task Main()
        {
            var console = new SystemsConsole();

            var time = SystemsTime.Create();
            var directions = UserDirections.Create();
            var food = new RandomFoodPositioningService();
            var size = Math.Min(console.Height, console.Width);
            var initialSnakePosition = (3, 5);
            var initialDirection = Direction.Down;
            var stage = new Stage(time, directions, food, size, initialSnakePosition, initialDirection);

            var renderer = new StageRenderer(console);

            stage.StageChangedEvent += (sender, args) =>
            {
                if (sender is Stage nextStage)
                    renderer.RenderNext(nextStage);
            };

            var tcs = new TaskCompletionSource();
            await tcs.Task;
        }
    }
}
