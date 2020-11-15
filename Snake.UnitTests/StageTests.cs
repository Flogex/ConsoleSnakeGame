using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using Xunit;
using static Snake.Direction;

namespace Snake.UnitTests
{
    public class StageTests
    {
        private static Stage CreateStage(
            IObservable<long> time = null,
            IObservable<Direction> directions = null,
            Position? initialPosition = null,
            Direction initialDirection = Right)
        {
            return new Stage(
                time ?? Observable.Empty<long>(),
                directions ?? Observable.Empty<Direction>(),
                initialPosition ?? (3, 3),
                initialDirection);
        }

        [Fact]
        public void WhenCreatingNewStage_ThenNewSnakeIsInitialized()
        {
            var stage = CreateStage();
            stage.Snake.Should().NotBeNull();
        }

        [Fact]
        public void WhenCreatingNewStage_HeadOfSnakeShouldBeOnInitialPosition()
        {
            var stage = CreateStage(initialPosition: (4, 2));
            stage.Snake.Head.Should().Be(new Position(4, 2));
        }

        [Theory]
        [InlineData(Left)]
        [InlineData(Right)]
        [InlineData(Up)]
        [InlineData(Down)]
        public void WhenCreatingNewStage_ThenCurrentDirectionShouldBeInitialDirection(Direction initialDirection)
        {
            var stage = CreateStage(initialDirection: initialDirection);
            stage.CurrentDirection.Should().Be(initialDirection);
        }

        [Fact]
        public void WhenSendingNewDirection_CurrentDirectionShouldReflectUpdate()
        {
            var directions = new Subject<Direction>();
            var stage = CreateStage(directions: directions);

            directions.OnNext(Up);

            stage.CurrentDirection.Should().Be(Up);
        }

        [Fact]
        public void WhenSendingMultipleNewDirection_CurrentDirectionShouldBeLastOneInStream()
        {
            var directions = new Subject<Direction>();
            var stage = CreateStage(directions: directions);

            directions.OnNext(Up);
            directions.OnNext(Down);

            stage.CurrentDirection.Should().Be(Down);
        }

        [Fact]
        public void WhenTimeUnitElapses_SnakeShouldMoveOneFieldInCurrentDirection()
        {
            var time = new Subject<long>();
            var stage = CreateStage(time: time, initialPosition: (5, 4), initialDirection: Right);

            time.OnNext(1);

            stage.Snake.Head.Should().Be(new Position(6, 4));
        }

        [Fact]
        public void WhenTwoTimeUnitsElapse_SnakeShouldMoveTwoFieldsInCurrentDirection()
        {
            var time = new Subject<long>();
            var stage = CreateStage(time: time, initialPosition: (5, 4), initialDirection: Up);

            time.OnNext(1);
            time.OnNext(2);

            stage.Snake.Head.Should().Be(new Position(5, 2));
        }

        [Fact]
        public void WhenCurrentDirectionChangesBetweenMoves_SnakeShouldMoveInNewDirection()
        {
            var time = new Subject<long>();
            var directions = new Subject<Direction>();
            var stage = CreateStage(time, directions, (5, 4), Down);

            time.OnNext(1);
            directions.OnNext(Left);
            time.OnNext(2);

            stage.Snake.Head.Should().Be(new Position(4, 5));
        }
    }
}
