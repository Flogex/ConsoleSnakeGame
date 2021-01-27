using ConsoleSnakeGame.Gameplay;
using ConsoleSnakeGame.GameObjects;

namespace ConsoleSnakeGame.Ports
{
    public interface IFoodPositionProvider
    {
        public Position GetNextPosition(Boundaries boundaries, Snake snake);
    }
}
