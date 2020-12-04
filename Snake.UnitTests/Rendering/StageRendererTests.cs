using Snake.GameObjects;
using Snake.Rendering;
using Snake.UnitTests.Fakes;
using static Snake.UnitTests.Helpers.SnakeGenerator;

namespace Snake.UnitTests.Rendering
{
    public partial class StageRendererTests
    {
        private readonly FakeConsole _console;
        private readonly StageRenderer _sut;
        private readonly Boundaries _boundaries;

        private readonly Snake _bogusSnake;
        private readonly Position _bogusFood;

        public StageRendererTests()
        {
            _console = new FakeConsole(10, 10);
            _sut = new StageRenderer(_console);
            _boundaries = new Boundaries(10);

            _bogusSnake = CreateSnakeAtPositions((8, 8));
            _bogusFood = new Position(7, 5);
        }
    }
}
