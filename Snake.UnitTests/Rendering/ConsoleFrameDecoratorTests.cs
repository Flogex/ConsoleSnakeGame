using NSubstitute;
using Snake.Rendering;
using Xunit;

namespace Snake.UnitTests.Rendering
{
    public class ConsoleFrameDecoratorTests
    {
        private readonly IConsole _inner;
        private readonly ConsoleFrameDecorator _sut;

        public ConsoleFrameDecoratorTests()
        {
            _inner = Substitute.For<IConsole>();
            _inner.Width.Returns(10);
            _inner.Height.Returns(10);
            _sut = new ConsoleFrameDecorator(_inner);
        }

        [Fact]
        public void WhenRemovingChar_PositionShouldBeCorrectlyTranslated()
        {
            _sut.RemoveCharAt(5, 5);

            const int borderWidth = ConsoleFrameDecorator.BorderWidth;
            var translatedPosition = new CursorPosition(5 + borderWidth, 5 + borderWidth);
            _inner.Received().RemoveCharAt(translatedPosition);
        }

        [Fact]
        public void WhenWritingChar_PositionShouldBeCorrectlyTranslated()
        {
            _sut.WriteAt('X', 5, 5);

            const int borderWidth = ConsoleFrameDecorator.BorderWidth;
            var translatedPosition = new CursorPosition(5 + borderWidth, 5 + borderWidth);
            _inner.Received().WriteAt('X', translatedPosition);
        }
    }
}
