using System;
using Xunit;
using MarsRoverCaseStudy;
using System.Collections.Generic;
using MarsRoverCaseStudy.Model;
using System.Collections;

namespace MarsRoverCaseStudy.Tests
{
    public class MarsMissionTests
    {
        [Theory]
        [ClassData(typeof(MarsMissionTestData))]
        public void MarsMissionTest(Coordinate plateauMaxCoordinate, List<Rover> roverList, string expected)
        {
            Program.MarsMission(new MarsMissionInput { PlateauMaxCoordinate = plateauMaxCoordinate, RoverList = roverList });
            var result = Program.CollectResult(roverList);

            Assert.Equal(expected, result);
        }

        [Theory]
        [ClassData(typeof(RoverHealthTestData))]
        public void RoverHealthTest(MarsMissionInput input, bool expectedOperationStatus)
        {
            Program.ExplorePlateau(input.PlateauMaxCoordinate, input.RoverList);

            Assert.Equal(expectedOperationStatus, input.RoverList[0].IsOperating);
        }

        public class MarsMissionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new Coordinate(5, 5),
                    new List<Rover>
                    {
                        new Rover(new Coordinate(1, 2), Direction.North, "LMLMLMLMM"),
                        new Rover(new Coordinate(3, 3), Direction.East, "MMRMMRMRRM")
                    },
                    "1 3 N\r\n5 1 E\r\n"
                };
                yield return new object[] {
                    new Coordinate(int.MaxValue, int.MaxValue),
                    new List<Rover>
                    {
                        new Rover(new Coordinate(1, 2), Direction.North, "LMLMLMLMM"),
                        new Rover(new Coordinate(3, 3), Direction.East, "MMRMMRMRRM"),
                        new Rover(new Coordinate(1, 2), Direction.East, "LMLMLMLMMRM")
                    },
                    "1 3 N\r\n5 1 E\r\n1 3 N (CRASHED)\r\n"
                };
                yield return new object[] {
                    new Coordinate(6, 7),
                    new List<Rover>
                    {
                        new Rover(new Coordinate(0, 0), Direction.West, "M")
                    },
                    "-1 0 W (CRASHED)\r\n"
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class RoverHealthTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                    new MarsMissionInput
                    {
                        PlateauMaxCoordinate = new Coordinate(5, 5),
                        RoverList = new List<Rover>
                        {
                            new Rover(new Coordinate(0, 0), Direction.West, "M")
                        }
                    },
                    false
                };
                yield return new object[] {
                    new MarsMissionInput
                    {
                        PlateauMaxCoordinate = new Coordinate(1, 1),
                        RoverList = new List<Rover>
                        {
                            new Rover(new Coordinate(0, 0), Direction.North, "MRMRML")
                        }
                    },
                    true
                };
                yield return new object[] {
                    new MarsMissionInput
                    {
                        PlateauMaxCoordinate = new Coordinate(1, 1),
                        RoverList = new List<Rover>
                        {
                            new Rover(new Coordinate(0, 0), Direction.North, "MRMLMM")
                        }
                    },
                    false
                };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
