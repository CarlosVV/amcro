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
                var input = source.ReadToEnd();
                request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            }

            Console.WriteLine("Executing commands");
            var result = myRobot.Run(request);

            Console.WriteLine("Generating output file");
            using (var dest = new StreamWriter(args[1]))
            {
                var output = JsonConvert.SerializeObject(result);
                dest.Write(output);
            }

            Console.WriteLine("end of execution");
        }
    }
}
