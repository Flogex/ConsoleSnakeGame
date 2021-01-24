using FluentAssertions;
using FsCheck.Xunit;
using Snake.RealWorld;
using System;

namespace Snake.UnitTests.RealWorld
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
