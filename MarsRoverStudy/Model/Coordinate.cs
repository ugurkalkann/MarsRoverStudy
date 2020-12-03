using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverCaseStudy.Model
{
    public class Coordinate
    {
        public long CoordinateX { get; set; }
        public long CoordinateY { get; set; }

        public Coordinate(long coordinateX, long coordinateY)
        {
            this.CoordinateX = coordinateX;
            this.CoordinateY = coordinateY;
        }
    }
}
