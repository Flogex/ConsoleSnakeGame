namespace Snake
{
    public interface IFoodPositioningService
    {
        public Position GetNextPosition(Boundaries boundaries, Snake snake);
    }
}
