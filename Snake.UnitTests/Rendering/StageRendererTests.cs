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

        private readonly Snake _dummySnake;
        private readonly Position _dummyFood;

        public StageRendererTests()
        {
            _console = new FakeConsole(10, 10);
            _sut = new StageRenderer(_console);
            _boundaries = new Boundaries(_console.Height, _console.Width);

            _dummySnake = CreateSnakeAtPositions((8, 8));
            _dummyFood = new Position(7, 5);
        }
    }
}
