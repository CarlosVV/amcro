﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cleaning_robot
{
    public class Position : Coordinate
    {
        [JsonProperty("facing", Order = 3)]       
        public string Facing { get; set; }
    }
}
