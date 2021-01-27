using System;
using System.Threading.Tasks;
using ConsoleSnakeGame.GameObjects;
using ConsoleSnakeGame.Ports;

namespace ConsoleSnakeGame.Gameplay
{
    public class Stage : IStage
    {
        private readonly IFoodPositionProvider _foodPositions;

        private readonly IObservable<long> _time;
        private readonly IObservable<Direction> _directionStream;

        private IDisposable? _timeSubscription;
        private IDisposable? _directionStreamSubscription;

        private readonly TaskCompletionSource _tcs = new TaskCompletionSource();

        public Stage(
            IObservable<long> time,
            IObservable<Direction> directionStream,
            IFoodPositionProvider foodPositions, //TODO Food must be far enough from head, in bounds, not at position where tail is, reachable
            Boundaries boundaries,
            InitialSnakeState initialSnakeState)
        {
            _time = time;
            _directionStream = directionStream;

            Boundaries = boundaries;
            Snake = new Snake(initialSnakeState.HeadPosition);
            CurrentDirection = initialSnakeState.Direction;

            _foodPositions = foodPositions;
            CurrentFoodPosition = foodPositions.GetNextPosition(Boundaries, Snake);
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
            _directionStreamSubscription = _directionStream.Subscribe(next => CurrentDirection = next);

            return _tcs.Task;
        }

        private void HandleTimeElapsed()
        {
            Snake = Snake.Move(CurrentDirection);

            if (Referee.HasPlayerLost(Snake, (Boundaries)Boundaries))
                FinishGame();

            if (Snake.Head == CurrentFoodPosition)
            {
                Snake = Snake.Eat();
                CurrentFoodPosition = _foodPositions.GetNextPosition(Boundaries, Snake);
            }

            StageChangedEvent?.Invoke(this, EventArgs.Empty);
        }

        private void FinishGame()
        {
            GameOver = true;
            _timeSubscription!.Dispose();
            _directionStreamSubscription!.Dispose();
            _tcs.SetResult();
        }
    }
}
