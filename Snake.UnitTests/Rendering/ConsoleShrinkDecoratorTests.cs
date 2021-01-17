using FluentAssertions;
using NSubstitute;
using Snake.Rendering;
using System;
using Xunit;

namespace Snake.UnitTests.Rendering
{
    public class ConsoleShrinkDecoratorTests
    {
        private readonly IConsole _inner;
        private readonly ConsoleShrinkDecorator _sut;

        public ConsoleShrinkDecoratorTests()
        {
            _inner = Substitute.For<IConsole>();
            _inner.Width.Returns(10);
            _inner.Height.Returns(10);
            _sut = new ConsoleShrinkDecorator(5, 5, _inner);
        }

        [Fact]
        public void WhenWidthPassedToCtorIsGreaterThanHeightOfInner_ThenArgumentExceptionShouldBeThrown()
        {
            Action act = () => new ConsoleShrinkDecorator(_inner.Width + 1, _inner.Height, _inner);
            act.Should().Throw<ArgumentException>()
                .And.ParamName.Should().Be("width");
        }

        [Fact]
        public void WhenHeightPassedToCtorIsGreaterThanHeightOfInner_ThenArgumentExceptionShouldBeThrown()
        {
            Action act = () => new ConsoleShrinkDecorator(_inner.Width, _inner.Height + 1, _inner);
            act.Should().Throw<ArgumentException>()
                .And.ParamName.Should().Be("height");
        }

        [Theory]
        [InlineData(3, 7)]
        [InlineData(7, 3)]
        [InlineData(7, 7)]
        public void WhenRemovingCharOutsideConsole_ThenArgumentExceptionShouldBeThrown(
            int row,
            int column)
        {
            Action act = () => _sut.RemoveCharAt(row, column);
            act.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(3, 7)]
        [InlineData(7, 3)]
        [InlineData(7, 7)]
        public void WhenWritingCharOutsideConsole_ThenArgumentExceptionShouldBeThrown(
            int row,
            int column)
        {
            Action act = () => _sut.WriteAt('X', row, column);
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void WhenCursorPositionIsInsideBounds_ThenInnerConsoleShouldBeCalled()
        {
            var position = new CursorPosition(3, 3);

            _sut.RemoveCharAt(position);
            _inner.Received().RemoveCharAt(position);

            _sut.WriteAt('X', position);
            _inner.Received().WriteAt('X', position);

            _sut.WriteAt("Y", position);
            _inner.Received().WriteAt("Y", position);
        }
    }
}
