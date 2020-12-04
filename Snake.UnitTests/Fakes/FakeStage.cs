using Snake.GameObjects;

namespace Snake.UnitTests.Fakes
{
    internal class FakeStage : IStage
    {
        public Boundaries Boundaries { get; set; }

        public Snake Snake { get; set; }

        public Position CurrentFoodPosition { get; set; }

        public bool GameOver { get; set; }
    }
}
