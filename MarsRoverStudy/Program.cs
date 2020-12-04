using MarsRoverCaseStudy.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MarsRoverCaseStudy
{
    public class Program
    {
        static void Main(string[] args)
        {
            MarsMission(ReadInput());

            Console.ReadKey();
        }

        public static void MarsMission(MarsMissionInput input)
        {
            ExplorePlateau(input.PlateauMaxCoordinate, input.RoverList);

            Console.WriteLine(CollectResult(input.RoverList));
        }

        public static void ExplorePlateau(Coordinate plateauMaxCoordinate, List<Rover> roverList)
        {
            var roverFinalCoordinates = new List<Coordinate>(); //Coordinates of the other rovers which finished their job.

            foreach (var currentRover in roverList)
            {
                foreach (var instruction in currentRover.InstructionText.ToCharArray())
                {
                    if (instruction == 'M')
                    {
                        currentRover.MoveForward();
                        if (!CheckCoordinates(currentRover, plateauMaxCoordinate, roverFinalCoordinates))
                        {
                            //if the rover is crashed, do not follow the rest of instructions.
                            break;
                        }
                    }
                    else
                    {
                        currentRover.Turn(instruction);
                    }
                }

                roverFinalCoordinates.Add(currentRover.CurrentCoordinate);
            }
        }

        public static bool CheckCoordinates(Rover rover, Coordinate plateauMaxCoordinate, IEnumerable<Coordinate> roverFinalCoordinates)
        {
            if (rover.CurrentCoordinate.CoordinateX > plateauMaxCoordinate.CoordinateX ||
                rover.CurrentCoordinate.CoordinateY > plateauMaxCoordinate.CoordinateY ||
                rover.CurrentCoordinate.CoordinateX < 0 ||
                rover.CurrentCoordinate.CoordinateY < 0)
            {
                Debug.WriteLine("Rover fell off the cliff.");
                rover.IsOperating = false;
                return false;
            }

            bool isRoverCoordinateOccupied = roverFinalCoordinates
                .Any(i =>
                    i.CoordinateX == rover.CurrentCoordinate.CoordinateX &&
                    i.CoordinateY == rover.CurrentCoordinate.CoordinateY
                    );

            if (isRoverCoordinateOccupied)
            {
                Debug.WriteLine("Rover collided with another one.");
                rover.IsOperating = false;
                return false;
            }

            return true;
        }

        public static MarsMissionInput ReadInput()
        {
            MarsMissionInput input;
            Console.WriteLine("Type '1' to read input from input.txt (in project build path); skip to read input from string variable in source code:");
            if (Console.ReadLine().Trim() == "1")
            {
                input = ReadInputFromFile();
            }
            else
            {
                string inputStr =
                        @"5 5
                        1 2 N
                        LMLMLMLMM
                        3 3 E
                        MMRMMRMRRM
                        1 2 E
                        LMLMLMLMMRM";
                input = ReadInputFromString(inputStr);
            }

            return input;
        }

        public static MarsMissionInput ReadInputFromString(string inputStr)
        {
            MarsMissionInput input = new MarsMissionInput();

            List<string> inputList = inputStr.Split(Environment.NewLine).ToList();
            var plateauMaxCoordinates = inputList[0].Trim().Split(' ');
            input.PlateauMaxCoordinate = new Coordinate(long.Parse(plateauMaxCoordinates[0]), long.Parse(plateauMaxCoordinates[1]));

            for (var i = 1; i < inputList.Count; i++)
            {
                string inputLine = inputList[i].Trim();
                var roverInitialPosition = inputLine.Split(' ');
                i++;
                string roverInstructions = inputList[i].Trim();
                input.RoverList.Add(new Rover(new Coordinate(long.Parse(roverInitialPosition[0]), long.Parse(roverInitialPosition[1])), (Direction)char.Parse(roverInitialPosition[2]), roverInstructions));
            }

            return input;
        }

        public static MarsMissionInput ReadInputFromFile()
        {
            MarsMissionInput input = new MarsMissionInput();

            /*
             * input.txt must be in project output directory
             * "bin\Debug\netcoreapp3.1\" for Debug configuration.
             * "bin\netcoreapp3.1\" for Release configuration.
             */
            string inputFileName = $"{Environment.CurrentDirectory}\\input.txt";
            Console.WriteLine($"Reading input from {inputFileName}");
            using (StreamReader reader = File.OpenText($"{inputFileName}"))
            {
                var plateauMaxCoordinates = reader.ReadLine().Trim().Split(' ');
                input.PlateauMaxCoordinate = new Coordinate(long.Parse(plateauMaxCoordinates[0]), long.Parse(plateauMaxCoordinates[1]));

                //read rover info
                while (!reader.EndOfStream)
                {
                    var roverInitialPosition = reader.ReadLine().Trim().Split(' ');
                    input.RoverList.Add(new Rover(new Coordinate(long.Parse(roverInitialPosition[0]), long.Parse(roverInitialPosition[1])), (Direction)char.Parse(roverInitialPosition[2]), reader.ReadLine().Trim()));

                }
            }

            return input;
        }

        public static string CollectResult(List<Rover> roverList)
        {
            StringBuilder result = new StringBuilder();
            foreach (var rover in roverList)
            {
                result.AppendLine($"{rover.CurrentCoordinate.CoordinateX} {rover.CurrentCoordinate.CoordinateY} {(char)rover.Direction}" + (rover.IsOperating ? "" : " (CRASHED)"));
            }
            return result.ToString();
        }
    }
}
