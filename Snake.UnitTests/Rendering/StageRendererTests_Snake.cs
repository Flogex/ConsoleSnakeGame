using FluentAssertions;
using Snake.GameObjects;
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
                CurrentFoodPosition = _dummyFood
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
        public void WhenNextSnakeIsLongerThenPreviousOne_ThenCompleteSnakeShouldBeDrawn()
        {
            var initialSnake = CreateSnakeAtPositions((3, 3), (4, 3));
            var stage = CreateStageWithSnake(initialSnake);

            _sut.RenderNext(stage);

            var nextSnake = initialSnake.Move(Up).Eat();
            stage.Snake = nextSnake;

            _sut.RenderNext(stage);

            _console.ArePositionsSet((3, 2), (3, 3), (4, 3)).Should().BeTrue();
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

        [Fact]
        public void WhenSnakeAteFood_ThenCompleteGrownSnakeShouldBeRendered()
        {
            var snake = CreateSnakeAtPositions((2, 2), (2, 3), (3, 3));

            var initialStage = new FakeStage
            {
                Boundaries = _boundaries,
                CurrentFoodPosition = (2, 1),
                Snake = snake
            };

            _sut.RenderNext(initialStage);

            var nextStage = new FakeStage
            {
                Boundaries = _boundaries,
                CurrentFoodPosition = (5, 5),
                Snake = snake.Move(Up).Eat()
            };

            _sut.RenderNext(nextStage);

            var expectedPositions = new Position[] { (2, 1), (2, 2), (2, 3), (3, 3) };
            _console.GetSetPositions().Should().Contain(expectedPositions);
        }
    }
}
