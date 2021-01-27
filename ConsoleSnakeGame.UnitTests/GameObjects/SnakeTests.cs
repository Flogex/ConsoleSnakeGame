using FluentAssertions;
using ConsoleSnakeGame.GameObjects;
using Xunit;
using static ConsoleSnakeGame.GameObjects.Direction;

namespace ConsoleSnakeGame.UnitTests.GameObjects
{
    public partial class SnakeTests
    {
        private static readonly Position _anyPosition = new Position(2, 2);
        private static readonly Direction _anyDirection = Right;

        [Fact]
        public void WhenTwoSnakesWithLength1HaveSameHeadPosition_TheyShouldBeEqual()
        {
            var snake1 = new Snake(3, 5);
            var snake2 = new Snake(3, 5);

            var areEqual = snake1 == snake2;
            areEqual.Should().BeTrue();

            var hashCode1 = snake1.GetHashCode();
            var hashCode2 = snake2.GetHashCode();
            hashCode1.Should().Be(hashCode2);
        }

        [Fact]
        public void WhenTwoSnakesWithLength1HaveDifferentHeadPosition_TheyShouldNotBeEqual()
        {
            var snake1 = new Snake(3, 5);
            var snake2 = new Snake(3, 3);

            var areNotEqual = snake1 != snake2;
            areNotEqual.Should().BeTrue();

            var hashCode1 = snake1.GetHashCode();
            var hashCode2 = snake2.GetHashCode();
            hashCode1.Should().NotBe(hashCode2);
        }

        [Fact]
        public void WhenSnakeBodiesAreAtSamePosition_ThenSnakesShouldBeEqual()
        {
            var snake1 = new Snake(3, 5).Move(Left).Eat().Move(Up).Move(Up).Eat().Move(Right);
            var snake2 = new Snake(2, 4).Move(Up).Eat().Move(Right).Eat();

            var areEqual = snake1 == snake2;
            areEqual.Should().BeTrue();

            var hashCode1 = snake1.GetHashCode();
            var hashCode2 = snake2.GetHashCode();
            hashCode1.Should().Be(hashCode2);
        }
    }
}
