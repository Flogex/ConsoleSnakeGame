using FluentAssertions;
using ConsoleSnakeGame.GameObjects;
using ConsoleSnakeGame.UnitTests.Fakes;
using Xunit;

namespace ConsoleSnakeGame.UnitTests.Rendering
{
    public partial class StageRendererTests
    {
        private FakeStage CreateStageWithFood(Position foodPosition)
        {
            return new FakeStage
            {
                CurrentFoodPosition = foodPosition,
                Boundaries = _boundaries,
                Snake = _dummySnake
            };
        }

        [Fact]
        public void WhenRenderingFirstStage_ThenInitialFoodShouldBeDrawn()
        {
            var initialFoodPosition = new Position(3, 5);
            var stage = CreateStageWithFood(initialFoodPosition);

            _sut.RenderNext(stage);

            _console.ArePositionsSet((3, 5)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNewFood_ThenNewFoodShouldBeDrawnAtCorrectPosition()
        {
            var initialFoodPosition = new Position(3, 5);
            var stage = CreateStageWithFood(initialFoodPosition);

            _sut.RenderNext(stage);

            var nextFoodPosition = new Position(1, 1);
            stage.CurrentFoodPosition = nextFoodPosition;

            _sut.RenderNext(stage);

            _console.ArePositionsSet((1, 1)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNewFood_ThenOldFoodShouldBeErasedFromConsole()
        {
            var initialFoodPosition = new Position(3, 5);
            var stage = CreateStageWithFood(initialFoodPosition);

            _sut.RenderNext(stage);

            var nextFoodPosition = new Position(1, 1);
            stage.CurrentFoodPosition = nextFoodPosition;

            _sut.RenderNext(stage);

            _console.ArePositionsSet((3, 5)).Should().BeFalse();
        }
    }
}
