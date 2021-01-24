using System;
using System.Linq;
using System.Threading.Tasks;
using Snake.GameObjects;

namespace Snake
{
    public class Stage : IStage
    {
        private readonly IFoodPositioningService _foodPositions;

        private readonly IObservable<long> _time;
        private readonly IObservable<Direction> _directionStream;

        private IDisposable? _timeSubscription;
        private IDisposable? _directionStreamSubscription;

        private readonly TaskCompletionSource _tcs = new TaskCompletionSource();

        public Stage(
            IObservable<long> time,
            IObservable<Direction> directionStream,
            IFoodPositioningService foodPositions, //TODO Food must be far enough from head, in bounds, not at position where tail is, reachable
            Boundaries boundaries,
            Position initialSnakePosition,
            Direction initialDirection)
        {
            _time = time;
            _directionStream = directionStream;

            this.Snake = new Snake(initialSnakePosition);
            this.Boundaries = boundaries;
            this.CurrentDirection = initialDirection;

            _foodPositions = foodPositions;
            this.CurrentFoodPosition = foodPositions.GetNextPosition(this.Boundaries, this.Snake);
        }

        public Snake Snake { get; private set; }

        public Boundaries Boundaries { get; }

        public Direction CurrentDirection { get; private set; }

        public Position CurrentFoodPosition { get; private set; }

        public bool GameOver { get; private set; }

        public event EventHandler? StageChangedEvent;

        public Task Start()
        {
            _timeSubscription = _time.Subscribe(_ => HandleTimeElapsed());
            _directionStreamSubscription = _directionStream.Subscribe(next => this.CurrentDirection = next);

            return _tcs.Task;
        }

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
            _timeSubscription!.Dispose();
            _directionStreamSubscription!.Dispose();
            _tcs.SetResult();
        }
    }
}
