using FluentAssertions;
using Snake.UnitTests.Fakes;
using Xunit;
using static Snake.Direction;
using static Snake.UnitTests.Helpers.SnakeGenerator;

namespace Snake.UnitTests.Rendering
{
    public partial class StageRendererTests
    {
        private FakeStage CreateStageWithSnake(Snake snake)
        {
            return new FakeStage
            {
                Snake = snake,
                Boundaries = _boundaries,
                CurrentFoodPosition = _bogusFood
            };
        }

        [Fact]
        public void WhenRenderingFirstStage_ThenSnakeShouldBeDrawn()
        {
            var snake = CreateSnakeAtPositions((3, 3), (4, 3), (4, 4), (3, 4));
            var stage = CreateStageWithSnake(snake);

            _sut.RenderNext(stage);

            _console.ArePositionsSet((3, 3), (4, 3), (4, 4), (3, 4)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNextSnake_ThenNewSnakeShouldBeDrawn()
        {
            var initialSnake = CreateSnakeAtPositions((3, 3), (4, 3), (4, 4), (3, 4));
            var stage = CreateStageWithSnake(initialSnake);

            _sut.RenderNext(stage);

            var nextSnake = initialSnake.Move(Up);
            stage.Snake = nextSnake;

            _sut.RenderNext(stage);

            _console.ArePositionsSet((3, 2), (3, 3), (4, 3), (4, 4)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNextSnake_ThenOldSnakeShouldBeErased()
        {
            var initialSnake = CreateSnakeAtPositions((3, 3), (4, 3), (4, 4), (3, 4));
            var stage = CreateStageWithSnake(initialSnake);

            _sut.RenderNext(stage);

            var nextSnake = initialSnake.Move(Up);
            stage.Snake = nextSnake;

            _sut.RenderNext(stage);

            _console.ArePositionsSet((3, 4)).Should().BeFalse();
        }
    }
}
