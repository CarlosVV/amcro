using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class CardinalPoint
    {
        private string Name { get; set;}
        private Facing Facing { get; set; }
        public CardinalPoint(Facing facing)
        {
            Facing = facing;
        }
        public string ToString()
        {
            return Name;
        }
    }
}
