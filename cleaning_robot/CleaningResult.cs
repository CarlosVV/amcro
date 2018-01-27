using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleaning_robot
{
    public class CleaningResult
    {
        public List<Position> Visited { get; set; }
        public List<Position> Cleaned { get; set; }
        public Position Final { get; set; }
        public int Battery { get; set; }
    }
}
