using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    /// <summary>
    /// Enum to store the cardinal points where the Robot is looking at
    /// </summary>
    public enum Facing
    {
        N = 0, S = 1, E = 2, W = 3
    }

    /// <summary>
    /// Enum to Store Information about the available actions to execute: Turn Left (TL), Turn Right (TR), Advance (A), Back (B) a
    /// and Clean (C). These actions will be sent in the commands array stored in the Request. 
    /// </summary>
    public enum RobotAction
    {
        TL = 0, TR = 1, A = 2, B = 3, C = 4
    }

    /// <summary>
    /// Main Class that will run the commands supplied 
    /// </summary>

    public class Robot
    {
        /// <summary>
        /// Contains the Map used by the Robot to Clean 
        /// </summary>
        protected string[][] Map { get; set; }
        /// <summary>
        /// Contains the coordinates X, Y where the Robot walks
        /// </summary>
        protected List<Coordinate> Visited { get; set; } = new List<Coordinate>();
        /// <summary>
        /// Contains the coordinates X, Y where the cleans
        /// </summary>
        protected List<Coordinate> Cleaned { get; set; } = new List<Coordinate>();
        /// <summary>
        /// Contains a matrix to store the result facing when an command (Turn Left or Turn Right) is applied knowing its initial facing
        /// </summary>
        protected string[][] FacingActionResult { get; set; } = new string[][] { new string[2] { "W", "E" }, new string[2] { "E", "W" }, new string[2] { "N", "S" }, new string[2] { "S", "N" } };
        /// <summary>
        /// Battery object that stores Status and Gets the Comsumption of Battery for each action
        /// </summary>
        protected Battery Battery { get; set; }
        /// <summary>
        /// Current Coordinate resulting of executing the Current Command
        /// </summary>
        protected Coordinate CurrentCoordinate { get; set; }
        /// <summary>
        /// Previous Coordinate used as backup when the current coordinate has an invalid value or state
        /// </summary>
        protected Coordinate PreviousCoordinate { get; set; }
        /// <summary>
        /// Current Facing where the Robot is looking
        /// </summary>
        protected Facing CurrentFacing { get; set; }
        /// <summary>
        /// Current command that is being executed
        /// </summary>
        protected RobotAction CurrentRobotAction { get; set; }
        /// <summary>
        /// Boolean that indicates if the Robot is running a Obstacle Backoff Strategy
        /// </summary>
        protected bool IsRunningObstacleBackOffStrategy { get; set; }
        /// <summary>
        /// Matrix to map the strategies commands if there are an obstacle
        /// </summary>
        protected string[][] AlternativeActions { get; set; } = new string[][] { new string[] { "TR", "A" }, new string[] { "TL", "B", "TR", "A" }, new string[] { "TL", "TL", "A" }, new string[] { "TR", "B", "TR", "A" }, new string[] { "TL", "TL", "A" } };
        /// <summary>
        /// Indicates if there an obstacle when executing the current command. An obstacle depends of Current Coordinate, Map Position X and Y and Batery Status
        /// </summary>
        /// <returns></returns>
        protected bool IsObstacle() => (CurrentCoordinate.X < 0 || CurrentCoordinate.Y < 0 || CurrentCoordinate.X >= Map.GetLength(0) ||CurrentCoordinate.Y >= Map.GetLength(0) || Map[CurrentCoordinate.Y][CurrentCoordinate.X] == "C" ||  Map[CurrentCoordinate.Y][CurrentCoordinate.X] == "null" || Battery.Status - Battery.Consumption(CurrentRobotAction) < 0);

        public Robot(Battery battery)
        {
            Battery = battery;
            CurrentCoordinate = new Position();
        }

        /// <summary>
        ///  Main Method to be used by passing the complete structure request with instructions
        /// </summary>
        /// <param name="request">Request class containing the Map, Start, Commands and Battery Status</param>
        /// <returns></returns>
        public CleaningResult Run(CleaningRequest request)
        {
            if (request == null)
            {
                return null;
            }

            var result = new CleaningResult();
            Map = request.Map;
            Battery = new Battery { Status = request.Battery };
            CurrentCoordinate = request.Start;
            PreviousCoordinate = CurrentCoordinate;
            CurrentFacing = (Facing)Enum.Parse(typeof(Facing), request.Start.Facing);
            Visited.Add(new Coordinate { X = CurrentCoordinate.X, Y = CurrentCoordinate.Y });

            foreach (var command in request.Commands)
            {
                CurrentRobotAction = (RobotAction)Enum.Parse(typeof(RobotAction), command);
                ExecuteCommand();

                if (IsObstacle())
                {
                    CurrentCoordinate = PreviousCoordinate;
                    IsRunningObstacleBackOffStrategy = true;
                    var alternativeIndex = 0;
                    do
                    {
                        var altCommands = AlternativeActions[alternativeIndex];
                        foreach (var altCommand in altCommands)
                        {
                            CurrentRobotAction = (RobotAction)Enum.Parse(typeof(RobotAction), altCommand);
                            ExecuteCommand();
                        }

                        if (!IsObstacle())
                        {
                            break;
                        }
                        else
                        {
                            CurrentCoordinate = PreviousCoordinate;
                            IsRunningObstacleBackOffStrategy = true;
                        }

                        alternativeIndex++;
                    }
                    while (IsRunningObstacleBackOffStrategy || alternativeIndex <= AlternativeActions.GetLength(0));

                    IsRunningObstacleBackOffStrategy = false;
                }
            }

            result.Visited = Visited.OrderBy(m => m.X).ThenBy(m => m.Y).ToList();
            result.Cleaned = Cleaned.OrderBy(m => m.X).ThenBy(m => m.Y).ToList();
            result.Final = new Position { X = CurrentCoordinate.X, Y = CurrentCoordinate.Y, Facing = CurrentFacing.ToString() };
            result.Battery = Battery.Status;

            return result;
        }
        private void ExecuteCommand()
        {
            if (Battery.Status - Battery.Consumption(CurrentRobotAction) < 0)
            {
                return;
            }

            Battery.Status = Battery.Status - Battery.Consumption(CurrentRobotAction);

            if (CurrentRobotAction == RobotAction.A || CurrentRobotAction == RobotAction.B)
            {
                PreviousCoordinate = CurrentCoordinate;
                CurrentCoordinate = GetResultCoordinate(CurrentFacing, CurrentRobotAction, CurrentCoordinate);
            }
            else if (CurrentRobotAction == RobotAction.TL || CurrentRobotAction == RobotAction.TR)
            {
                CurrentFacing = (Facing)Enum.Parse(typeof(Facing), FacingActionResult[(int)CurrentFacing][(int)CurrentRobotAction]);
            }

            if (!IsObstacle())
            {
                if ((CurrentRobotAction == RobotAction.A || CurrentRobotAction == RobotAction.B) && (!Visited.Any(m => m.X == CurrentCoordinate.X && m.Y == CurrentCoordinate.Y)))
                {
                    Visited.Add(CurrentCoordinate);
                }
                else if ((CurrentRobotAction == RobotAction.C) && (!Cleaned.Any(m => m.X == CurrentCoordinate.X && m.Y == CurrentCoordinate.Y)))
                {
                    Cleaned.Add(CurrentCoordinate);
                }
            }
        }
        private Coordinate GetResultCoordinate(Facing facing, RobotAction action, Coordinate coordinate)
        {
            if ((facing == Facing.N && action == RobotAction.B) || (facing == Facing.S && action == RobotAction.A))
            {
                return new Coordinate { X = coordinate.X, Y = coordinate.Y + 1 };
            }

            if ((facing == Facing.N && action == RobotAction.A) || (facing == Facing.S && action == RobotAction.B))
            {
                return new Coordinate { X = coordinate.X, Y = coordinate.Y - 1 };
            }

            if ((facing == Facing.E && action == RobotAction.B) || (facing == Facing.W && action == RobotAction.A))
            {
                return new Coordinate { X = coordinate.X - 1, Y = coordinate.Y };
            }

            if ((facing == Facing.E && action == RobotAction.A) || (facing == Facing.W && action == RobotAction.B))
            {
                return new Coordinate { X = coordinate.X + 1, Y = coordinate.Y };
            }

            return coordinate;
        }
    }
}
