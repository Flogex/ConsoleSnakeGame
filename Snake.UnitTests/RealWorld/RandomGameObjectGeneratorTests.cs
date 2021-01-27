using FluentAssertions;
using FsCheck.Xunit;
using ConsoleSnakeGame.Gameplay;
using ConsoleSnakeGame.RealWorld;
using System;

namespace ConsoleSnakeGame.UnitTests.RealWorld
{
    public class RandomGameObjectGeneratorTests
    {
        [Property]
        public void GetPosition_EveryPositionShouldBeInBoundaries()
        {
            var boundaries = new Boundaries(8, 8);
            var randomPosition = RandomGameObjectGenerator.GetPosition(boundaries);
            boundaries.IsOutOfBounds(randomPosition).Should().BeFalse();
        }

        [Property]
        public void GetDirection_ReturnedValueShouldBeValidDirection()
        {
            var randomDirection = RandomGameObjectGenerator.GetDirection();
            Enum.IsDefined(randomDirection).Should().BeTrue();
        }
    }
}
