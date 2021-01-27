using ConsoleSnakeGame.GameObjects;

namespace ConsoleSnakeGame.Gameplay
{
    public readonly struct InitialSnakeState
    {
        public InitialSnakeState(Position initialHeadPosition, Direction initialDirection)
        {
            HeadPosition = initialHeadPosition;
            Direction = initialDirection;
        }

        public Position HeadPosition { get; }

        public Direction Direction { get; }
    }
}
