using System;

namespace Snake
{
    public class Stage
    {
        private readonly IDisposable _timeSubscription;
        private readonly IDisposable _directionStreamSubscription;

        public Stage(
            IObservable<long> time,
            IObservable<Direction> directionStream,
            int size,
            Position initialPosition,
            Direction initialDirection)
        {
            this.Snake = new Snake(initialPosition);
            this.Boundaries = new Boundaries(size);
            this.CurrentDirection = initialDirection;

            _timeSubscription = HandleTime(time);
            _directionStreamSubscription = HandleDirectionStream(directionStream);
        }

        public Snake Snake { get; private set; }

        public Boundaries Boundaries { get; }

        public Direction CurrentDirection { get; private set; }

        public bool GameOver { get; private set; }

        private IDisposable HandleTime(IObservable<long> time)
        {
            //TODO Food
            return time.Subscribe(_ =>
            {
                this.Snake = this.Snake.Move(this.CurrentDirection);

                if (this.Boundaries.IsOutOfBounds(this.Snake.Head))
                {
                    FinishGame();
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
