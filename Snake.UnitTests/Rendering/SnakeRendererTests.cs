using System;
using FluentAssertions;
using Snake.GameObjects;
using Snake.Rendering;
using Snake.UnitTests.Fakes;
using Xunit;
using static Snake.Direction;

namespace Snake.UnitTests.Rendering
{
    public class SnakeRendererTests
    {
        private readonly FakeConsole _console;
        private readonly SnakeRenderer _sut;

        public SnakeRendererTests()
        {
            _console = new FakeConsole(10, 10);
            _sut = new SnakeRenderer(_console);
        }

        /// <param name="positions">List starts with head</param>
        private static Snake CreateSnakeAtPositions(params Position[] positions)
        {
            var snake = new Snake(positions[^1]);

            for (var i = positions.Length - 1; i > 0; i--)
            {
                var direction = GetDirectionFromPositionVector(positions[i], positions[i - 1]);
                snake = snake.Move(direction).Eat();
            }

            return snake;
        }

        private static Direction GetDirectionFromPositionVector(Position start, Position end)
        {
            var vector = (end.X - start.X, end.Y - start.Y);

            return vector switch
            {
                ( < 0, 0) => Left,
                ( > 0, 0) => Right,
                (0, < 0) => Up,
                (0, > 0) => Down,
                _ => throw new ArgumentException("Unknown direction")
            };
        }

        [Fact]
        public void WhenRenderingFirstSnake_ThenCompleteSnakeShouldBeDrawn()
        {
            var snake = CreateSnakeAtPositions((3, 3), (4, 3), (4, 4), (3, 4));

            _sut.RenderNext(snake);

            _console.ArePositionsSet((3, 3), (4, 3), (4, 4), (3, 4)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNextSnake_ThenNewSnakeShouldBeDrawn()
        {
            var initialSnake = CreateSnakeAtPositions((3, 3), (4, 3), (4, 4), (3, 4));
            _sut.RenderNext(initialSnake);

            var nextSnake = initialSnake.Move(Up);
            _sut.RenderNext(nextSnake);

            _console.ArePositionsSet((3, 2), (3, 3), (4, 3), (4, 4)).Should().BeTrue();
        }

        [Fact]
        public void WhenRenderingNextSnake_ThenOldSnakeShouldBeErased()
        {
            var initialSnake = CreateSnakeAtPositions((3, 3), (4, 3), (4, 4), (3, 4));
            _sut.RenderNext(initialSnake);

            var nextSnake = initialSnake.Move(Up);
            _sut.RenderNext(nextSnake);

            _console.ArePositionsSet((3, 4)).Should().BeFalse();
        }
    }
}
