using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class CleaningRequest
    {
        public string[][] Map { get; set; }
        public Position  Start { get; set; }
        public string[] Commands { get; set; }
        public int Battery { get; set; }
    }
}
