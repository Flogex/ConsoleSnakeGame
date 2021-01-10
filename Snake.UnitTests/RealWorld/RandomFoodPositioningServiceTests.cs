using System;
using FluentAssertions;
using FsCheck.Xunit;
using Snake.GameObjects;
using Snake.RealWorld;
using Xunit;
using static Snake.Direction;

namespace Snake.UnitTests.RealWorld
{
    public class RandomFoodPositioningServiceTests
    {
        private readonly IFoodPositioningService _sut = new RandomFoodPositioningService();
        private readonly Snake _dummySnake = new Snake(1, 1);

        [Property]
        public void FoodPositionShouldBeInBoundaries()
        {
            var boundaries = new Boundaries(4);
            var position = _sut.GetNextPosition(boundaries, _dummySnake);
            boundaries.IsOutOfBounds(position).Should().BeFalse();
        }

        [Property]
        public void FoodPositionShouldNotCollideWithSnake()
        {
            var boundaries = new Boundaries(5);
            var snake = new Snake(3, 3)
                .Move(Left).Eat()
                .Move(Up).Eat();

            var position = _sut.GetNextPosition(boundaries, snake);

            position.Should()
                .NotBe(new Position(3, 3)).And
                .NotBe(new Position(2, 3)).And
                .NotBe(new Position(2, 2));
        }

        [Fact]
        public void WhenSnakeFillsGameBoardCompletely_ThenInvalidOperationExceptionShouldBeThrown()
        {
            var boundaries = new Boundaries(2);
            var snake = new Snake(0, 0)
                .Move(Right).Eat()
                .Move(Down).Eat()
                .Move(Left).Eat();

            Action act = () => _sut.GetNextPosition(boundaries, snake);

            act.Should().Throw<InvalidOperationException>();
        }
    }
}
