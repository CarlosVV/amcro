﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleaning_robot
{
    public class CleaningRequest
    {
        public string[][] Map { get; set; }
        public Position  Position { get; set; }
        public string[] Commands { get; set; }
        public int Battery { get; set; }
    }
}