using FluentAssertions;
using Snake.UnitTests.Fakes;
using Xunit;
using static Snake.Direction;

namespace Snake.UnitTests
{
    public partial class StageTests
    {
        public class WhenCreatingNewStage
        {
            [Fact]
            public void ThenNewSnakeIsInitialized()
            {
                var stage = CreateStage();
                stage.Snake.Should().NotBeNull();
            }

            [Fact]
            public void ThenHeadOfSnakeShouldBeOnInitialPosition()
            {
                var stage = CreateStage(initialPosition: (4, 2));
                stage.Snake.Head.Should().Be(new Position(4, 2));
            }

            [Fact]
            public void ThenBoundariesArePassed()
            {
                var stage = CreateStage(stageSize: 4);
                stage.Boundaries.Should().Be(new Boundaries(4));
            }

            [Fact]
            public void ThenFoodShouldBeOnPositionProvidedByPositioningService()
            {
                var foodPositions = new FakeFoodPositioningService((3, 3));
                var stage = CreateStage(foodPositions: foodPositions);

                stage.CurrentFoodPosition.Should().Be(new Position(3, 3));
            }

            [Theory]
            [InlineData(Left)]
            [InlineData(Right)]
            [InlineData(Up)]
            [InlineData(Down)]
            public void ThenCurrentDirectionShouldBeInitialDirection(Direction initialDirection)
            {
                var stage = CreateStage(initialDirection: initialDirection);
                stage.CurrentDirection.Should().Be(initialDirection);
            }
        }
    }
}
