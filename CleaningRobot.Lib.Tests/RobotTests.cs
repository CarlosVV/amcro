using Microsoft.VisualStudio.TestTools.UnitTesting;
using CleaningRobot.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CleaningRobot.Lib.Tests
{
    [TestClass()]
    public class RobotTests
    {
        [TestMethod()]
        public void Test_Run_Sample_01_00_Basic_01()
        {
            var myRobot = new Robot(new Battery());
            var request = null as CleaningRequest ;                        
           
            var result = myRobot.Run(request);

            Assert.IsNull(result);
        }
        [TestMethod()]
        public void Test_Run_Sample_01_00_Basic_02()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result);
        }
        [TestMethod()]
        public void Test_Run_Sample_01_01_Visited()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result.Visited);
            Assert.AreEqual(result.Visited.Count(), 3);
        }
        [TestMethod()]
        public void Test_Run_Sample_01_02_Cleaned()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result.Cleaned);
            Assert.AreEqual(result.Cleaned.Count(), 2);
        }
        [TestMethod()]
        public void Test_Run_Sample_01_03_Final()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result.Final);
            Assert.AreEqual(result.Final.X, 2);
            Assert.AreEqual(result.Final.Y, 0);
        }
        [TestMethod()]
        public void Test_Run_Sample_01_04_Battery()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.AreEqual(result.Battery, 54);
        }
        public void Test_Run_Sample_01_05_Map_Null()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";
            
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);

            request.Map = null;

            var result = myRobot.Run(request);

            Assert.IsNull(result);
        }

        public void Test_Run_Sample_01_05_Map_Null_2()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";

            request = JsonConvert.DeserializeObject<CleaningRequest>(input);

            request.Map[0] = null;

            var result = myRobot.Run(request);

            Assert.IsNull(result);
        }

        public void Test_Run_Sample_01_05_Map_Null_3()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";

            request = JsonConvert.DeserializeObject<CleaningRequest>(input);

            request.Map[0] = new[] { "X" };

            var result = myRobot.Run(request);

            Assert.IsNull(result);
        }

        public void Test_Run_Sample_01_05_Map_Not_Valid()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{ \"map\": [[ \"S\", \"S\", \"S\", \"S\" ], [ \"S\", \"S\", \"C\", \"S\" ],[ \"S\", \"S\", \"S\", \"S\" ],[ \"S\", \"null\", \"S\", \"S\" ]],\"start\": {\"X\": 3, \"Y\": 0, \"facing\": \"N\" }, \"commands\": [ \"TL\", \"A\", \"C\", \"A\", \"C\", \"TR\", \"A\", \"C\" ],\"battery\":  80 }";

            request = JsonConvert.DeserializeObject<CleaningRequest>(input);

            request.Map[0] = new[] { "a", "b", "A", "A" };

            var result = myRobot.Run(request);

            Assert.IsNull(result);
        }

        [TestMethod()]
        public void Test_Run_Sample_02_00_Basic()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{  \"map\": [    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"S\", \"C\", \"S\"],    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"null\", \"S\", \"S\"]  ],  \"start\": {\"X\": 3, \"Y\": 1, \"facing\": \"S\"},  \"commands\": [ \"TR\",\"A\",\"C\",\"A\",\"C\",\"TR\",\"A\",\"C\"],  \"battery\": 1094}";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void Test_Run_Sample_02_01_Visited()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{  \"map\": [    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"S\", \"C\", \"S\"],    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"null\", \"S\", \"S\"]  ],  \"start\": {\"X\": 3, \"Y\": 1, \"facing\": \"S\"},  \"commands\": [ \"TR\",\"A\",\"C\",\"A\",\"C\",\"TR\",\"A\",\"C\"],  \"battery\": 1094}";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result.Visited);
            Assert.AreEqual(result.Visited.Count(), 4);
        }
        [TestMethod()]
        public void Test_Run_Sample_02_02_Cleaned()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{  \"map\": [    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"S\", \"C\", \"S\"],    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"null\", \"S\", \"S\"]  ],  \"start\": {\"X\": 3, \"Y\": 1, \"facing\": \"S\"},  \"commands\": [ \"TR\",\"A\",\"C\",\"A\",\"C\",\"TR\",\"A\",\"C\"],  \"battery\": 1094}";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result.Cleaned);
            Assert.AreEqual(result.Cleaned.Count(), 3);
        }
        [TestMethod()]
        public void Test_Run_Sample_02_03_Final()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{  \"map\": [    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"S\", \"C\", \"S\"],    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"null\", \"S\", \"S\"]  ],  \"start\": {\"X\": 3, \"Y\": 1, \"facing\": \"S\"},  \"commands\": [ \"TR\",\"A\",\"C\",\"A\",\"C\",\"TR\",\"A\",\"C\"],  \"battery\": 1094}";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.IsNotNull(result.Final);
            Assert.AreEqual(result.Final.X, 3);
            Assert.AreEqual(result.Final.Y, 2);
        }

        [TestMethod()]
        public void Test_Run_Sample_02_04_Battery()
        {
            var myRobot = new Robot(new Battery());
            var request = new CleaningRequest();

            var input = "{  \"map\": [    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"S\", \"C\", \"S\"],    [\"S\", \"S\", \"S\", \"S\"],    [\"S\", \"null\", \"S\", \"S\"]  ],  \"start\": {\"X\": 3, \"Y\": 1, \"facing\": \"S\"},  \"commands\": [ \"TR\",\"A\",\"C\",\"A\",\"C\",\"TR\",\"A\",\"C\"],  \"battery\": 1094}";
            request = JsonConvert.DeserializeObject<CleaningRequest>(input);
            var result = myRobot.Run(request);

            Assert.AreEqual(result.Battery, 1040);
        }
    }
}