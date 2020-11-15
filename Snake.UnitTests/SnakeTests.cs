using System.Linq;
using FluentAssertions;
using Xunit;

namespace Snake.UnitTests
{
    public class SnakeTests
    {
        private static readonly Position _anyPosition = new Position(2, 2);

        public class WhenCreatingNewSnake
        {
            [Theory]
            [InlineData(1, 1)]
            public void ThenHeadShouldBeAtGivenPosition(int x, int y)
            {
                var snake = new Snake(x, y);
                snake.Head.X.Should().Be(x);
                snake.Head.Y.Should().Be(y);
            }

            [Fact]
            public void ThenBodyShouldContainHeadAsSingleElement()
            {
                var snake = new Snake(2, 2);
                snake.Body.Should().ContainSingle(p => p == new Position(2, 2));
            }

            [Fact]
            public void ThenLengthShouldBe1()
            {
                var snake = new Snake(_anyPosition);
                snake.Length.Should().Be(1);
            }
        }

        public class WhenEating
        {
            private static readonly Direction _anyDirection = Direction.Right;

            [Fact]
            public void OperationShouldBeImmutable()
            {
                var initialSnake = new Snake(_anyPosition).Move(_anyDirection);
                var initialBody = initialSnake.Body.ToArray();

                var snakeAfterMovingAndEating = initialSnake.Eat();

                initialSnake.Body.Should().Equal(initialBody);

                snakeAfterMovingAndEating.Should().NotBe(initialSnake);
            }

            [Fact]
            public void AndSnakeHasNoTail_LengthOfSnakeShouldBe2()
            {
                var snake = new Snake(_anyPosition).Move(_anyDirection);
                var snakeAfterMeal = snake.Eat();
                snakeAfterMeal.Length.Should().Be(2);
            }

            [Fact]
            public void AndSnakeHasLengthOf2_LengthOfSnakeShouldBe3()
            {
                var snake = new Snake(_anyPosition)
                    .Move(_anyDirection)
                    .Eat()
                    .Move(_anyDirection);

                var snakeAfterMeal = snake.Eat();

                snakeAfterMeal.Length.Should().Be(3);
            }

            [Theory]
            [InlineData(2, 2)]
            [InlineData(2, 3)]
            public void NewTailElementShouldAppearOnPreviousPositionOfLastTailPart(int x, int y)
            {
                var snake = new Snake(x, y).Move(_anyDirection);
                var snakeAfterMeal = snake.Eat();
                snakeAfterMeal.Body.Last().Should().Be(new Position(x, y));
            }
        }
    }
}
