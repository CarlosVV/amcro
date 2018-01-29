using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public class Battery
    {
        public int Status { get; set; }
        public int Consumption(RobotAction action) => batteryConsumption[action];
        private Dictionary<RobotAction, int> batteryConsumption { get; set; } = new Dictionary<RobotAction, int>();
        public Battery()
        {
            batteryConsumption.Add(RobotAction.TL, 1);
            batteryConsumption.Add(RobotAction.TR, 1);
            batteryConsumption.Add(RobotAction.A, 2);
            batteryConsumption.Add(RobotAction.B, 3);
            batteryConsumption.Add(RobotAction.C, 5);
        }
    }
}
