using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverCaseStudy.Model
{
    public class MarsMissionInput
    {
        public Coordinate PlateauMaxCoordinate { get; set; }
        public List<Rover> RoverList { get; set; }

        public MarsMissionInput()
        {
            this.RoverList = new List<Rover>();
        }
    }
}
