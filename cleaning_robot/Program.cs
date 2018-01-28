using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
            var request = new CleaningRequest();

            using (var source = new StreamReader(args[0]))
            {
                var content = source.ReadToEnd();
                request = JsonConvert.DeserializeObject<CleaningRequest>(content);
            }               

            var result = myRobot.Run(request);
        }
    }
}
