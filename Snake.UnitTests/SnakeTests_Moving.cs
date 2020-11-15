using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;
using static Snake.Direction;

namespace Snake.UnitTests
{
    public class WhenMovingSnakeWithoutTail
    {
        private readonly Snake _snake = new Snake(2, 2);

        [Fact]
        public void ToTheLeft_ThenXCoordinateOfHeadShouldBeDecreasedBy1()
        {
            _snake.Move(Left);

            _snake.Head.X.Should().Be(1);
            _snake.Head.Y.Should().Be(2);
        }

        [Fact]
        public void ToTheRight_ThenXCoordinateOfHeadShouldBeIncreasedBy1()
        {
            _snake.Move(Right);

            _snake.Head.X.Should().Be(3);
            _snake.Head.Y.Should().Be(2);
        }

        [Fact]
        public void Up_ThenYCoordinateOfHeadShouldBeDecreasedBy1()
        {
            _snake.Move(Up);

            _snake.Head.Y.Should().Be(1);
            _snake.Head.X.Should().Be(2);
        }

        [Fact]
        public void Down_ThenYCoordinateOfHeadShouldBeIncreasedBy1()
        {
            _snake.Move(Down);

            _snake.Head.Y.Should().Be(3);
            _snake.Head.X.Should().Be(2);
        }
    }

    public class WhenMovingSnake
    {
        // ∀ s ∈ Snakes:
        //    s' = s.Move(Any)
        //    s'.Body[1..] == s.Body[0..^1]
        [Theory]
        [InlineData(3, 3, new[] { Left }, Left)]
        [InlineData(1, 1, new[] { Right, Right }, Down)]
        [InlineData(3, 1, new[] { Down, Left, Up }, Left)]
        [InlineData(3, 2, new[] { Down, Left, Up, Up }, Right)]
        public void ThenAllElementsShouldMoveToPositionOfPredecessor(
            int initialX,
            int initialY,
            Direction[] directions,
            Direction actDirection)
        {
            var snake = new Snake(initialX, initialY);
            foreach (var direction in directions)
            {
                snake.Move(direction);
                snake.Eat();
            }

            var bodyBeforeMove = snake.Body.ToArray();

            snake.Move(actDirection);

            var bodyAfterMove = snake.Body.ToArray();

            bodyAfterMove[1..].Should().Equal(bodyBeforeMove[..^1]);
        }

        [Theory]
        [MemberData(nameof(MoveSnakeWithLength3BySequenceTestData))]
        public void WithLength3ByGivenSequence_AllPartsShouldBeAtExpectedPosition(
            Direction[] sequence,
            Position[] expectedPartPositions)
        {
            // Parts of snake are initially at [(4, 6), (4, 5), (5, 5)]
            var snake = new Snake(5, 5);
            snake.Move(Left);
            snake.Eat();
            snake.Move(Down);
            snake.Eat();

            foreach (var direction in sequence)
                snake.Move(direction);

            snake.Body.Should().Equal(expectedPartPositions);
        }

        public static IEnumerable<object[]> MoveSnakeWithLength3BySequenceTestData()
        {
            // Given: Snake with parts at positions [(4, 6), (4, 5), (5, 5)]
            var testData = new[]
            {
                (
                    Sequence: new[] { Down },
                    ExpectedPartPositions: new Position[] { (4, 7), (4, 6), (4, 5) }
                ),
                (
                    Sequence: new[] { Right, Up },
                    ExpectedPartPositions: new Position[] { (5, 5), (5, 6), (4, 6) }
                ),
                (
                    Sequence: new[] { Left, Left, Left },
                    ExpectedPartPositions: new Position[] { (1, 6), (2, 6), (3, 6) }
                ),
                (
                    Sequence: new[] { Right, Down, Left, Down },
                    ExpectedPartPositions: new Position[] { (4, 8), (4, 7), (5, 7) }
                )
            };

            return testData.Select(data => new object[] { data.Sequence, data.ExpectedPartPositions });
        }

        [Theory]
        [MemberData(nameof(MoveSnakeWithLength5BySequenceTestData))]
        public void WithLength5ByGivenSequence_AllPartsShouldBeAtExpectedPosition(
            Direction[] sequence,
            Position[] expectedPartPositions)
        {
            // Parts of snake are initially at [(3, 7), (4, 7), (4, 6), (4, 5), (5, 5)]
            var snake = new Snake(5, 5);
            snake.Move(Left);
            snake.Eat();
            snake.Move(Down);
            snake.Eat();
            snake.Move(Down);
            snake.Eat();
            snake.Move(Left);
            snake.Eat();

            foreach (var direction in sequence)
                snake.Move(direction);

            snake.Body.Should().Equal(expectedPartPositions);
        }

        public static IEnumerable<object[]> MoveSnakeWithLength5BySequenceTestData()
        {
            // Given: Snake with parts at positions [(3, 7), (4, 7), (4, 6), (4, 5), (5, 5)]
            var testData = new[]
            {
                (
                    Sequence: new[] { Down },
                    ExpectedPartPositions: new Position[] { (3, 8), (3, 7), (4, 7), (4, 6), (4, 5) }
                ),
                (
                    Sequence: new[] { Up, Up },
                    ExpectedPartPositions: new Position[] { (3, 5), (3, 6), (3, 7), (4, 7), (4, 6) }
                ),
                (
                    Sequence: new[] { Left, Left, Left },
                    ExpectedPartPositions: new Position[] { (0, 7), (1, 7), (2, 7), (3, 7), (4, 7) }
                ),
                (
                    Sequence: new[] { Down, Right, Right, Up },
                    ExpectedPartPositions: new Position[] { (5, 7), (5, 8), (4, 8), (3, 8), (3, 7) }
                )
            };

            return testData.Select(data => new object[] { data.Sequence, data.ExpectedPartPositions });
        }
    }
}
