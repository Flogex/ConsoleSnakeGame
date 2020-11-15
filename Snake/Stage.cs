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
            Position initialPosition,
            Direction initialDirection)
        {
            this.Snake = new Snake(initialPosition);
            this.CurrentDirection = initialDirection;

            _timeSubscription = HandleTime(time);
            _directionStreamSubscription = HandleDirectionStream(directionStream);
        }

        public Snake Snake { get; private set; }

        public Direction CurrentDirection { get; private set; }

        private IDisposable HandleTime(IObservable<long> time)
        {
            return time.Subscribe(_ =>
            {
                this.Snake = this.Snake.Move(this.CurrentDirection);
            });
        }

        private IDisposable HandleDirectionStream(IObservable<Direction> directionStream) =>
            directionStream.Subscribe(next => this.CurrentDirection = next);
    }
}
