﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleaning_robot
{
    class Program
    {
        static void Main(string[] args)
        {
            var myRobot = new Robot();
            var result = myRobot.Run(new CleaningRequest());
        }
    }
}
