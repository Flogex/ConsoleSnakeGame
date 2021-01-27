using ConsoleSnakeGame.GameObjects;
using FluentAssertions;
using Xunit;

namespace ConsoleSnakeGame.UnitTests.GameObjects
{
    public partial class SnakeTests
    {
        public class WhenCreatingNewSnake
        {
            [Theory]
            [InlineData(1, 1)]
            [InlineData(2, 1)]
            [InlineData(1, 2)]
            public void ThenHeadShouldBeAtGivenPosition(int x, int y)
            {
                var snake = new Snake(x, y);
                snake.Head.X.Should().Be(x);
                snake.Head.Y.Should().Be(y);
            }

            [Fact]
            public void ThenBodyShouldContainHeadAsSingleElement()
            {
                var snake = new Snake(_anyPosition);
                snake.Body.Should().ContainSingle();
            }

            [Fact]
            public void ThenLengthShouldBe1()
            {
                var snake = new Snake(_anyPosition);
                snake.Length.Should().Be(1);
            }
        }
    }
}
