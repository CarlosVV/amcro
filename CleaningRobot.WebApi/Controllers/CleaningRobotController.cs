using CleaningRobot.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CleaningRobot.WebApi.Controllers
{
    public class CleaningRobotController : ApiController
    {
        protected Robot robot = new Robot(new Battery());
        public CleaningRobotController()
        {

        }
        [Route("robot/clean")]
        public IHttpActionResult Post(CleaningRequest request)
        {
            return Ok(robot.Run(request));
        }
    }
}
