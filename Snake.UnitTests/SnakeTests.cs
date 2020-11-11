using FluentAssertions;
using Xunit;

namespace Snake.UnitTests
{
    public class SnakeTests
    {
        private static readonly Position _anyPosition = new Position(1, 1);

        public class WhenCreatingNewSnake
        {
            [Theory]
            [InlineData(1, 1)]
            public void ThenHeadShouldBeAtGivenPosition(int x, int y)
            {
                var headPosition = new Position(x, y);
                var snake = new Snake(headPosition);

                snake.Head.X.Should().Be(x);
                snake.Head.Y.Should().Be(y);
            }
        }

        public class WhenMovingSnakeWithoutTail
        {
            private readonly Snake _snake = new Snake(2, 2);

            [Fact]
            public void ToTheLeft_ThenXCoordinateOfHeadShouldBeDecreasedBy1()
            {
                _snake.Move(Direction.Left);

                _snake.Head.X.Should().Be(1);
                _snake.Head.Y.Should().Be(2);
            }

            [Fact]
            public void ToTheRight_ThenXCoordinateOfHeadShouldBeIncreasedBy1()
            {
                _snake.Move(Direction.Right);

                _snake.Head.X.Should().Be(3);
                _snake.Head.Y.Should().Be(2);
            }

            [Fact]
            public void Up_ThenYCoordinateOfHeadShouldBeDecreasedBy1()
            {
                _snake.Move(Direction.Up);

                _snake.Head.Y.Should().Be(1);
                _snake.Head.X.Should().Be(2);
            }

            [Fact]
            public void Down_ThenYCoordinateOfHeadShouldBeIncreasedBy1()
            {
                _snake.Move(Direction.Down);

                _snake.Head.Y.Should().Be(3);
                _snake.Head.X.Should().Be(2);
            }
        }
    }
}
