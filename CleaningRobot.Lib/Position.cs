using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    /// <summary>
    /// Position in the space to clean (X, Y) and Facing (North, South, East or West)
    /// </summary>
    public class Position : Coordinate
    {
        [JsonProperty("facing", Order = 3)]       
        public string Facing { get; set; }
    }
}
