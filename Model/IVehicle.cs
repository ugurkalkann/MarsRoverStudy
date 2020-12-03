using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoverCaseStudy.Model
{
    interface IVehicle
    {
        void MoveForward();
        void Turn(char direction); //'R' or 'L'
    }
}
