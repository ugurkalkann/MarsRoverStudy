using Xunit;
using MarsRoverCaseStudy.Model;
using System.Collections.Generic;
using System;

namespace MarsRoverCaseStudy.Tests
{
    public class RoverTests
    {
        [Theory]
        [InlineData(Direction.North, 'L', Direction.West)]
        [InlineData(Direction.North, 'R', Direction.East)]
        [InlineData(Direction.East, 'L', Direction.North)]
        [InlineData(Direction.East, 'R', Direction.South)]
        [InlineData(Direction.South, 'L', Direction.East)]
        [InlineData(Direction.South, 'R', Direction.West)]
        [InlineData(Direction.West, 'L', Direction.South)]
        [InlineData(Direction.West, 'R', Direction.North)]
        public void Turn(Direction initialDirection, char turnInstrunction, Direction expectedDirection)
        {
            Coordinate initialCoordinate = new Coordinate(0, 0);
            var rover = new Rover(initialCoordinate, initialDirection);

            rover.Turn(turnInstrunction);

            Assert.Equal(expectedDirection, rover.Direction);
        }

        [Theory]
        [InlineData(1, 3, Direction.North, 1, 4)]
        [InlineData(0, 0, Direction.East, 1, 0)]
        [InlineData(4, 2, Direction.South, 4, 1)]
        [InlineData(4, 2, Direction.West, 3, 2)]
        public void MoveForward(int initialCoordinateX, int initialCoordinateY, Direction roverDirection, long expectedCoordinateX, long expectedCoordinateY)
        {
            Coordinate initialCoordinate = new Coordinate(initialCoordinateX, initialCoordinateY);
            var rover = new Rover(initialCoordinate, roverDirection);

            rover.MoveForward();

            Assert.Equal(new List<long> { expectedCoordinateX, expectedCoordinateY }, new List<long> { rover.CurrentCoordinate.CoordinateX, rover.CurrentCoordinate.CoordinateY });
        }

        [Theory]
        [InlineData(Direction.North, 'K')]
        [InlineData(null, 'L')]
        public void InvalidTurn(Direction direction, char instruction)
        {
            Coordinate initialCoordinate = new Coordinate(0, 0);
            var rover = new Rover(initialCoordinate, direction);

            Assert.Throws<Exception>(() => rover.Turn(instruction));
        }
    }
}
