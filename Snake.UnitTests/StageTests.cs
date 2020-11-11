using FluentAssertions;
using Xunit;

namespace Snake.UnitTests
{
    public class StageTests
    {
        public class WhenCreatingNewStage
        {
            [Fact]
            public void ThenNewSnakeIsInitialized()
            {
                var stage = new Stage();
                stage.Snake.Should().NotBeNull();
            }
        }
    }
}
