using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Snake.GameObjects;
using Snake.UnitTests.Helpers;
using Xunit;
using static Snake.Direction;

namespace Snake.UnitTests.GameObjects
{
    public partial class SnakeTests
    {
        public class WhenMovingSnakeWithoutTail
        {
            // ∀s ∈ Snakes: s.Length == 1 ⇒
            //    s == s.Move(Left).Move(Right)
            //    s == s.Move(Up).Move(Down)
            [Theory]
            [InlineData(Left, Right)]
            [InlineData(Up, Down)]
            public void ThenMovingInOneDirectionAndThenInOppositeOneShouldHaveNoEffect(
                Direction direction,
                Direction oppositeDirection)
            {
                var initialSnake = new Snake(_anyPosition);
                var snakeAfterMove = initialSnake.Move(direction).Move(oppositeDirection);
                snakeAfterMove.Should().Be(initialSnake);
            }

            // ∀s ∈ Snakes ∀d1,d2 ∈ Direction: s.Length == 1 ⇒
            //     s.Move(d1).Move(d2) == s.Move(d2).Move(d1);
            [Theory]
            [MemberData(nameof(GetDirectionCombinations))]
            public void ThenOperationShouldBeCommutative(Direction d1, Direction d2)
            {
                var initialSnake = new Snake(_anyPosition);

                var snake1 = initialSnake.Move(d1).Move(d2);
                var snake2 = initialSnake.Move(d2).Move(d1);

                snake1.Should().Be(snake2);
            }

            public static IEnumerable<object[]> GetDirectionCombinations()
            {
                return Enum.GetValues<Direction>()
                    .CombineDistinctPairs()
                    .Select(p => new object[] { p.First, p.Second });
            }

            [Fact]
            public void ThenOperationShouldBeImmutable()
            {
                var initialSnake = new Snake(_anyPosition);
                var initialBody = initialSnake.Body.ToArray();

                var snakeAfterMoving = initialSnake.Move(_anyDirection);

                initialSnake.Body.Should().Equal(initialBody);
                snakeAfterMoving.Should().NotBe(initialSnake);
            }

            [Fact]
            public void ToTheLeft_ThenXCoordinateOfHeadShouldBeDecreasedBy1()
            {
                var snakeAfterMove = new Snake(2, 2).Move(Left);

                snakeAfterMove.Head.X.Should().Be(1);
                snakeAfterMove.Head.Y.Should().Be(2);
            }

            [Fact]
            public void ToTheRight_ThenXCoordinateOfHeadShouldBeIncreasedBy1()
            {
                var snakeAfterMove = new Snake(2, 2).Move(Right);

                snakeAfterMove.Head.X.Should().Be(3);
                snakeAfterMove.Head.Y.Should().Be(2);
            }

            [Fact]
            public void Up_ThenYCoordinateOfHeadShouldBeDecreasedBy1()
            {
                var snakeAfterMove = new Snake(2, 2).Move(Up);

                snakeAfterMove.Head.Y.Should().Be(1);
                snakeAfterMove.Head.X.Should().Be(2);
            }

            [Fact]
            public void Down_ThenYCoordinateOfHeadShouldBeIncreasedBy1()
            {
                var snakeAfterMove = new Snake(2, 2).Move(Down);

                snakeAfterMove.Head.Y.Should().Be(3);
                snakeAfterMove.Head.X.Should().Be(2);
            }
        }

        public class WhenMovingSnake
        {
            // ∀s ∈ Snakes ∀d ∈ Direction:
            //    s.Move(d).Body[1..] == s.Body[0..^1]
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
                var snakeBeforeMove = directions.Aggregate(
                    seed: new Snake(initialX, initialY),
                    func: (snake, direction) => snake.Move(direction).Eat());
                var bodyBeforeMove = snakeBeforeMove.Body.ToArray();

                var snakeAfterMove = snakeBeforeMove.Move(actDirection);
                var bodyAfterMove = snakeAfterMove.Body.ToArray();

                bodyAfterMove[1..].Should().Equal(bodyBeforeMove[..^1]);
            }

            [Theory]
            [MemberData(nameof(MoveSnakeWithLength3BySequenceTestData))]
            public void WithLength3ByGivenSequence_ThenAllPartsShouldBeAtExpectedPosition(
                Direction[] sequence,
                Position[] expectedPartPositions)
            {
                // Parts of snake are initially at [(4, 6), (4, 5), (5, 5)]
                var snake = new Snake(5, 5)
                    .Move(Left).Eat()
                    .Move(Down).Eat();

                foreach (var direction in sequence)
                    snake = snake.Move(direction);

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
            public void WithLength5ByGivenSequence_ThenAllPartsShouldBeAtExpectedPosition(
                Direction[] sequence,
                Position[] expectedPartPositions)
            {
                // Parts of snake are initially at [(3, 7), (4, 7), (4, 6), (4, 5), (5, 5)]
                var snake = new Snake(5, 5)
                    .Move(Left).Eat()
                    .Move(Down).Eat()
                    .Move(Down).Eat()
                    .Move(Left).Eat();

                foreach (var direction in sequence)
                    snake = snake.Move(direction);

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
}
