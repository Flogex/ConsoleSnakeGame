using System.Linq;
using FluentAssertions;
using Snake.GameObjects;
using Xunit;

namespace Snake.UnitTests.GameObjects
{
    public partial class SnakeTests
    {
        public class WhenEating
        {
            [Fact]
            public void ThenOperationShouldBeImmutable()
            {
                var initialSnake = new Snake(_anyPosition).Move(_anyDirection);
                var initialBody = initialSnake.Body.ToArray();

                var snakeAfterEating = initialSnake.Eat();

                initialSnake.Body.Should().Equal(initialBody);
                snakeAfterEating.Should().NotBe(initialSnake);
            }

            [Fact]
            public void AndSnakeHasNoTail_ThenLengthOfSnakeShouldBe2()
            {
                var snake = new Snake(_anyPosition).Move(_anyDirection);
                var snakeAfterMeal = snake.Eat();
                snakeAfterMeal.Length.Should().Be(2);
            }

            [Fact]
            public void AndSnakeHasLengthOf2_ThenLengthOfSnakeShouldBe3()
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
            [InlineData(3, 2)]
            [InlineData(2, 3)]
            public void ThenNewTailElementShouldAppearOnPreviousPositionOfLastTailPart(int x, int y)
            {
                var snake = new Snake(x, y).Move(_anyDirection);
                var snakeAfterMeal = snake.Eat();
                snakeAfterMeal.Body.Last().Should().Be(new Position(x, y));
            }
        }
    }
}
