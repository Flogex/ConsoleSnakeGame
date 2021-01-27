using System.Threading.Tasks;
using ConsoleSnakeGame.Gameplay;
using ConsoleSnakeGame.RealWorld;
using ConsoleSnakeGame.Rendering;

namespace ConsoleSnakeGame
{
    public class Program
    {
        public static Task Main()
        {
            var console = ConsoleCreator.Create();

            var time = SystemsTime.Create();
            var directions = UserDirections.Create();
            var food = new RandomFoodPositionProvider();
            var boundaries = new Boundaries(console.Height, console.Width);
            var snakeState = RandomGameObjectGenerator.GetSnakeState(boundaries);

            var stage = new Stage(time, directions, food, boundaries, snakeState);

            var renderer = new StageRenderer(console);
            StageChangedHandler.Initialize(stage, renderer);

            return stage.Start();
        }
    }
}
