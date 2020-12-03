using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverCaseStudy.Model
{
    public class Rover : IVehicle
    {
        public Coordinate CurrentCoordinate { get; }
        public Direction Direction { get; set; }
        public string InstructionText { get; }
        public bool IsOperating { get; set; } = true;

        public Rover(Coordinate initialCoordinate, Direction direction)
        {
            this.CurrentCoordinate = initialCoordinate;
            this.Direction = direction;
        }

        public Rover(Coordinate initialCoordinate, Direction direction, string instructions)
        {
            this.CurrentCoordinate = initialCoordinate;
            this.Direction = direction;
            this.InstructionText = instructions;
        }

        /// <summary>
        /// Sets the direction the vehicle will face.
        /// </summary>
        /// <param name="instruction">'R' or 'L'</param>
        public void Turn(char instruction)
        {
            if (instruction == 'R')
            {
                this.Direction = this.Direction switch
                {
                    Direction.North => Direction.East,
                    Direction.East => Direction.South,
                    Direction.South => Direction.West,
                    Direction.West => Direction.North,
                    _ => throw new Exception("Vehicle has no direction.")
                };
            }
            else if (instruction == 'L')
            {
                this.Direction = this.Direction switch
                {
                    Direction.North => Direction.West,
                    Direction.West => Direction.South,
                    Direction.South => Direction.East,
                    Direction.East => Direction.North,
                    _ => throw new Exception("Vehicle has no direction.")
                };
            }
            else
            {
                throw new Exception("Unknown instruction.");
            }
        }

        /// <summary>
        /// Sets the current coordinate of the vehicle.
        /// </summary>
        public void MoveForward()
        {
            if (this.Direction == Direction.North)
            {
                this.CurrentCoordinate.CoordinateY++;
            }
            else if (this.Direction == Direction.East)
            {
                this.CurrentCoordinate.CoordinateX++;
            }
            else if (this.Direction == Direction.South)
            {
                this.CurrentCoordinate.CoordinateY--;
            }
            else if (this.Direction == Direction.West)
            {
                this.CurrentCoordinate.CoordinateX--;
            }
            else
            {
                throw new Exception("Vehicle has no direction.");
            }
        }
    }

    public enum Direction
    {
        North = 'N',
        East = 'E',
        South = 'S',
        West = 'W'
    }
}
