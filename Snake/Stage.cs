using System;
using System.Linq;

namespace Snake
{
    public class Stage
    {
        private readonly IDisposable _timeSubscription;
        private readonly IDisposable _directionStreamSubscription;
        private readonly IFoodPositioningService _foodPositions;

        public Stage(
            IObservable<long> time,
            IObservable<Direction> directionStream,
            IFoodPositioningService foodPositions, //TODO Food must be far enough from head, in bounds, not at position where tail is, reachable
            int size,
            Position initialPosition,
            Direction initialDirection)
        {
            this.Snake = new Snake(initialPosition);
            this.Boundaries = new Boundaries(size);
            this.CurrentDirection = initialDirection;

            _foodPositions = foodPositions;
            this.CurrentFoodPosition = foodPositions.GetNextPosition(this.Boundaries, this.Snake);

            _timeSubscription = HandleTime(time);
            _directionStreamSubscription = HandleDirectionStream(directionStream);
        }

        public Snake Snake { get; private set; }

        public Boundaries Boundaries { get; }

        public Direction CurrentDirection { get; private set; }

        public Position CurrentFoodPosition { get; set; }

        public bool GameOver { get; private set; }

        private IDisposable HandleTime(IObservable<long> time)
        {
            return time.Subscribe(_ =>
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
            });
        }

        private void FinishGame()
        {
            this.GameOver = true;
            _timeSubscription.Dispose();
            _directionStreamSubscription.Dispose();
        }

        private IDisposable HandleDirectionStream(IObservable<Direction> directionStream) =>
            directionStream.Subscribe(next => this.CurrentDirection = next);
    }
}
