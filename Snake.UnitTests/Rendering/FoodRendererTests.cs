using FluentAssertions;
using Snake.GameObjects;
using Snake.Rendering;
using Snake.UnitTests.Fakes;
using Xunit;

namespace Snake.UnitTests.Rendering
{
    public class FoodRendererTests
    {
        [Fact]
        public void WhenRenderingFirstFood_ThenFoodShouldBeDrawnAtCorrectPosition()
        {
            var console = new FakeConsole(10, 10);
            var sut = new FoodRenderer(console);
            var nextFoodPosition = new Position(3, 5);

            sut.RenderNext(nextFoodPosition);

            console.ArePositionsSet((3, 5)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNewFood_ThenNewFoodShouldBeDrawnAtCorrectPosition()
        {
            var console = new FakeConsole(10, 10);
            var sut = new FoodRenderer(console);
            var initialFoodPosition = new Position(3, 5);
            sut.RenderNext(initialFoodPosition);

            var nextFoodPosition = new Position(1, 1);
            sut.RenderNext(nextFoodPosition);

            console.ArePositionsSet((1, 1)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNewFood_ThenOldFoodShouldBeErasedFromConsole()
        {
            var console = new FakeConsole(10, 10);
            var sut = new FoodRenderer(console);
            var initialFoodPosition = new Position(3, 5);
            sut.RenderNext(initialFoodPosition);

            var nextFoodPosition = new Position(1, 1);
            sut.RenderNext(nextFoodPosition);

            console.ArePositionsSet((3, 5)).Should().BeFalse();
        }
    }
}
