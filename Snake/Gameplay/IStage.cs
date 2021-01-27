using ConsoleSnakeGame.GameObjects;

namespace ConsoleSnakeGame.Gameplay
{
    public interface IStage
    {
        Boundaries Boundaries { get; }

        Snake Snake { get; }

        Position CurrentFoodPosition { get; }

        bool GameOver { get; }
    }
}