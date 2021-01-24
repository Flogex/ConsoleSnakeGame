using System;
using System.Threading.Tasks;
using Snake.RealWorld;
using Snake.Rendering;

namespace Snake
{
    class Program
    {
        public static Task Main()
        {
            var console = CreateConsole();

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

            return Task.Delay(10000);
        }

        private static IConsole CreateConsole()
        {
            var systemsConsole = new SystemsConsole();
            EnsureConsoleBigEnough(systemsConsole);

            return
                new ConsoleFrameDecorator(
                    new ConsoleShrinkDecorator(
                        Math.Min(20, systemsConsole.Width),
                        Math.Min(20, systemsConsole.Height),
                        systemsConsole));
        }

        private static void EnsureConsoleBigEnough(IConsole console)
        {
            // Needed for RandomPosition.GetNext

            if (console.Height < 5)
                throw new Exception("Console must at least have a height of 5.");

            if (console.Width < 5)
                throw new Exception("Console must at least have a width of 5.");
        }
    }
}
