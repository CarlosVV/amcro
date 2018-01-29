using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleaning_robot
{
    public class CleaningResult
    {
        public CleaningResult()
        {
            Visited = new List<Coordinate>();
            Cleaned = new List<Coordinate>();
            Final = new Position();       
        }

        [JsonProperty("visited")]
        public List<Coordinate> Visited { get; set; }
        [JsonProperty("cleaned")]
        public List<Coordinate> Cleaned { get; set; }
        [JsonProperty("final")]
        public Position Final { get; set; }
        [JsonProperty("battery")]
        public int Battery { get; set; }
    }
}
