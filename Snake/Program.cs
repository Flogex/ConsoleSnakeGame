using System;
using System.Threading.Tasks;
using Snake.RealWorld;
using Snake.Rendering;

namespace Snake
{
    public class Program
    {
        public static Task Main()
        {
            var console = CreateConsole();

            var time = SystemsTime.Create();
            var directions = UserDirections.Create();
            var food = new RandomFoodPositioningService();
            var boundaries = new Boundaries(console.Height, console.Width);
            var initialSnakePosition = RandomGameObjectGenerator.GetPosition(boundaries);
            var initialDirection = RandomGameObjectGenerator.GetDirection();

            var stage = new Stage(time, directions, food, boundaries,
                initialSnakePosition, initialDirection);

            var renderer = new StageRenderer(console);

            stage.StageChangedEvent += (sender, args) =>
            {
                if (sender is Stage nextStage)
                    renderer.RenderNext(nextStage);
            };

            return stage.Start();
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
