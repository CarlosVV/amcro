using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class Coordinate
    {
        [JsonProperty(Order = 1)]
        public int X { get; set; }
        [JsonProperty(Order = 2)]
        public int Y { get; set; }
    }
}
