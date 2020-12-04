using Snake.GameObjects;

namespace Snake
{
    public interface IStage
    {
        Boundaries Boundaries { get; }

        Snake Snake { get; }

        Position CurrentFoodPosition { get; }

        bool GameOver { get; }
    }
}