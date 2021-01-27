﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using FluentAssertions;
using ConsoleSnakeGame.Gameplay;
using ConsoleSnakeGame.GameObjects;
using ConsoleSnakeGame.Ports;
using ConsoleSnakeGame.UnitTests.Fakes;
using Xunit;
using static ConsoleSnakeGame.GameObjects.Direction;

namespace ConsoleSnakeGame.UnitTests
{
    public partial class StageTests
    {
        private static Stage CreateStage(
            IObservable<long> time = null,
            IObservable<Direction> directions = null,
            IFoodPositionProvider foodPositions = null,
            int stageSize = 10,
            Position? initialPosition = null,
            Direction initialDirection = Right)
        {
            var snakeState = new InitialSnakeState(
                initialPosition ?? (3, 3),
                initialDirection);

            var stage = new Stage(
                time ?? Observable.Empty<long>(),
                directions ?? Observable.Empty<Direction>(),
                foodPositions ?? new FakeFoodPositionProvider((8, 8)),
                new Boundaries(stageSize, stageSize),
                snakeState);

            _ = stage.Start();

            return stage;
        }

        [Fact]
        public void WhenSendingNewDirection_ThenCurrentDirectionShouldReflectUpdate()
        {
            var directions = new Subject<Direction>();
            var stage = CreateStage(directions: directions, initialDirection: Right);

            directions.OnNext(Up);

            stage.CurrentDirection.Should().Be(Up);
        }

        [Fact]
        public void WhenSendingMultipleNewDirections_ThenCurrentDirectionShouldBeLastOneSent()
        {
            var directions = new Subject<Direction>();
            var stage = CreateStage(directions: directions, initialDirection: Right);

            directions.OnNext(Up);
            directions.OnNext(Down);

            stage.CurrentDirection.Should().Be(Down);
        }

        [Fact]
        public void WhenTimeUnitElapses_ThenSnakeShouldMoveOneFieldInCurrentDirection()
        {
            var time = new Subject<long>();
            var stage = CreateStage(time: time, initialPosition: (5, 4), initialDirection: Right);

            time.OnNext(1);

            stage.Snake.Head.Should().Be(new Position(6, 4));
        }

        [Fact]
        public void WhenTwoTimeUnitsElapse_ThenSnakeShouldMoveTwoFieldsInCurrentDirection()
        {
            var time = new Subject<long>();
            var stage = CreateStage(time: time, initialPosition: (5, 4), initialDirection: Up);

            time.OnNext(1);
            time.OnNext(2);

            stage.Snake.Head.Should().Be(new Position(5, 2));
        }

        [Fact]
        public void WhenCurrentDirectionChangesBetweenMoves_ThenSnakeShouldMoveInNewDirection()
        {
            var time = new Subject<long>();
            var directions = new Subject<Direction>();
            var stage = CreateStage(time: time, directions: directions, initialPosition: (5, 4),
                initialDirection: Down);

            time.OnNext(1);
            directions.OnNext(Left);
            time.OnNext(2);

            stage.Snake.Head.Should().Be(new Position(4, 5));
        }

        [Theory]
        [InlineData(0, 0, 2, Up)]
        [InlineData(1, 1, 2, Down)]
        [InlineData(0, 0, 2, Left)]
        [InlineData(1, 1, 2, Right)]
        public void WhenSnakeMovesOffStage_ThenGameShouldBeOver(int initialX, int initialY,
            int stageSize, Direction initialDirection)
        {
            var time = new Subject<long>();
            var stage = CreateStage(time: time, stageSize: stageSize,
                initialPosition: (initialX, initialY), initialDirection: initialDirection);

            time.OnNext(1);

            stage.GameOver.Should().BeTrue();
        }

        [Fact]
        public void WhenSnakeMovesToPositionOfFood_ThenSnakeShouldGrow()
        {
            var time = new Subject<long>();
            var foodPositions = new FakeFoodPositionProvider((2, 3), (5, 2));
            var stage = CreateStage(
                time: time,
                foodPositions: foodPositions,
                initialPosition: (2, 2),
                initialDirection: Down);

            time.OnNext(1);

            stage.Snake.Length.Should().Be(2);
        }

        [Fact]
        public void WhenSnakeEatsFood_ThenFoodShouldAppearAtNextPosition()
        {
            var time = new Subject<long>();
            var foodPositions = new FakeFoodPositionProvider((2, 3), (5, 2));
            var stage = CreateStage(
                time: time,
                foodPositions: foodPositions,
                initialPosition: (2, 2),
                initialDirection: Down);

            time.OnNext(1);

            stage.CurrentFoodPosition.Should().Be(new Position(5, 2));
        }

        [Fact]
        public void WhenSnakeEatsFood_ThenSnakeTailShouldGrowToPreviousPosition()
        {
            var time = new Subject<long>();
            var foodPositions = new FakeFoodPositionProvider((2, 3), (5, 2));
            var stage = CreateStage(
                time: time,
                foodPositions: foodPositions,
                initialPosition: (2, 2),
                initialDirection: Down);

            time.OnNext(1);

            stage.Snake.Body.Should().Equal((2, 3), (2, 2));
        }

        [Fact]
        public void WhenSnakeEatsItself_ThenGameShouldBeOver()
        {
            var time = new Subject<long>();
            var directions = new Subject<Direction>();
            var foodPositions = new FakeFoodPositionProvider((0, 1), (0, 2), (1, 2), (1, 1), (9, 9));
            var stage = CreateStage(
                time: time,
                directions: directions,
                foodPositions: foodPositions,
                initialPosition: (0, 0),
                initialDirection: Down);

            time.OnNext(1); // Eat food at (0, 1)
            time.OnNext(2); // Eat food at (0, 2)
            directions.OnNext(Right);
            time.OnNext(3); // Eat food at (1, 2)
            directions.OnNext(Up);
            time.OnNext(4); // Eat food at (1, 1)
            directions.OnNext(Left);
            time.OnNext(5); // Eat tail

            stage.GameOver.Should().BeTrue();
        }

        [Fact]
        public void SnakeShouldBeAbleToMoveToCurrentPositionOfLastTailPart()
        {
            var time = new Subject<long>();
            var directions = new Subject<Direction>();
            var foodPositions = new FakeFoodPositionProvider((0, 1), (1, 1), (1, 0), (9, 9));
            var stage = CreateStage(
                time: time,
                directions: directions,
                foodPositions: foodPositions,
                initialPosition: (0, 0),
                initialDirection: Down);

            time.OnNext(1); // Eat food at (0, 1)
            directions.OnNext(Right);
            time.OnNext(3); // Eat food at (1, 1)
            directions.OnNext(Up);
            time.OnNext(4); // Eat food at (1, 0)

            var partPositions1 = new Position[] { (1, 0), (1, 1), (0, 1), (0, 0) };
            stage.Snake.Body.Should().Equal(partPositions1);

            directions.OnNext(Left);
            time.OnNext(5); // Snake head moves to (0, 0), but last tail part moves to (0, 1) simultaneously

            var partPositions2 = new Position[] { (0, 0), (1, 0), (1, 1), (0, 1) };
            stage.Snake.Body.Should().Equal(partPositions2);

            stage.GameOver.Should().BeFalse();
        }

        [Fact]
        public void WhenTimeElapsed_ThenStageShouldFireStageChangedEvent()
        {
            var time = new Subject<long>();
            var stage = CreateStage(time);

            using var monitor = stage.Monitor();

            time.OnNext(1);

            monitor.Should().Raise(nameof(Stage.StageChangedEvent)).WithSender(stage);
        }
    }
}
