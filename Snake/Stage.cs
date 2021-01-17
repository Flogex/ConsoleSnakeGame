using System;
using System.Linq;
using Snake.GameObjects;

namespace Snake
{
    public class Stage : IStage
    {
        private readonly IDisposable _timeSubscription;
        private readonly IDisposable _directionStreamSubscription;
        private readonly IFoodPositioningService _foodPositions;

        public Stage(
            IObservable<long> time,
            IObservable<Direction> directionStream,
            IFoodPositioningService foodPositions, //TODO Food must be far enough from head, in bounds, not at position where tail is, reachable
            Boundaries boundaries,
            Position initialSnakePosition,
            Direction initialDirection)
        {
            this.Snake = new Snake(initialSnakePosition);
            this.Boundaries = boundaries;
            this.CurrentDirection = initialDirection;

            _foodPositions = foodPositions;
            this.CurrentFoodPosition = foodPositions.GetNextPosition(this.Boundaries, this.Snake);

            _timeSubscription = time.Subscribe(_ => HandleTimeElapsed());
            _directionStreamSubscription = directionStream.Subscribe(next => this.CurrentDirection = next);
        }

        public Snake Snake { get; private set; }

        public Boundaries Boundaries { get; }

        public Direction CurrentDirection { get; private set; }

        public Position CurrentFoodPosition { get; private set; }

        public bool GameOver { get; private set; }

        public event EventHandler StageChangedEvent;

        private void HandleTimeElapsed()
        {
            this.Snake = this.Snake.Move(this.CurrentDirection);

            if (this.Boundaries.IsOutOfBounds(this.Snake.Head))
                FinishGame();

            if (this.Snake.Body.Skip(1).Contains(this.Snake.Head))
                FinishGame();

            if (this.Snake.Head == this.CurrentFoodPosition)
            {
                this.Snake = this.Snake.Eat();
                this.CurrentFoodPosition = _foodPositions.GetNextPosition(this.Boundaries, this.Snake);
            }

            this.StageChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void FinishGame()
        {
            this.GameOver = true;
            _timeSubscription.Dispose();
            _directionStreamSubscription.Dispose();
        }
    }
}
