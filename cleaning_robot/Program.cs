using CleaningRobot.Lib;
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
            Console.WriteLine("=======================================================================");
            Console.WriteLine("====================CLEANING ROBOT V1.O ===============================");
            Console.WriteLine("=============== CARLOS VALDERRAMA (cvalderramav@gmail.com)=============");
            Console.WriteLine();

            if (args.Length < 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must execute this program passing 2 arguments. Example:");
                Console.WriteLine("cleaning_robot source.json result.json");
                Console.WriteLine("where: source.json is the input file to execute the Simulation and result.json is the output file to generate the results.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }


            var input_path = args[0];
            var output_path = args[1];

            if (!File.Exists(input_path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Input file {input_path} does not exist");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            try
            {
                var myRobot = new Robot(new Battery());
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
                    var output = JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
                    dest.Write(output);
                }               
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Unhandled Exception executing program: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Hint: Review your input file. Possible malformed .JSON File");                
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("End of Execution");
            }         
        }
    }
}
