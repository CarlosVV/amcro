﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot.Lib
{
    public enum Facing
    {
        N = 0, S = 1, E = 2, W = 3
    }

    public enum RobotAction
    {
        TL = 0, TR = 1, A = 2, B = 3, C = 4
    }

    public class Robot
    {
        protected string[][] Map { get; set; }
        protected List<Coordinate> Visited { get; set; } = new List<Coordinate>();
        protected List<Coordinate> Cleaned { get; set; } = new List<Coordinate>();
        protected string[][] facingActionResult { get; set; } = new string[][] { new string[2] { "W", "E" }, new string[2] { "E", "W" }, new string[2] { "N", "S" }, new string[2] { "S", "N" } };
        protected Battery Battery { get; set; }
        protected Coordinate CurrentCoordinate { get; set; }
        protected Coordinate PreviousCoordinate { get; set; }
        protected Facing CurrentFacing { get; set; }
        protected RobotAction CurrentRobotAction { get; set; }
        protected bool IsRunningObstacleBackOffStrategy { get; set; }
        protected string[][] AlternativeActions { get; set; } = new string[][] { new string[] { "TR", "A" }, new string[] { "TL", "B", "TR", "A" }, new string[] { "TL", "TL", "A" }, new string[] { "TR", "B", "TR", "A" }, new string[] { "TL", "TL", "A" } };
        protected bool IsObstacle() => (CurrentCoordinate.X < 0 || CurrentCoordinate.Y < 0 || CurrentCoordinate.X >= Map.GetLength(0) ||CurrentCoordinate.Y >= Map.GetLength(0) || Map[CurrentCoordinate.Y][CurrentCoordinate.X] == "C" ||  Map[CurrentCoordinate.Y][CurrentCoordinate.X] == "null" || Battery.Status - Battery.Consumption(CurrentRobotAction) < 0);

        public Robot(Battery battery)
        {
            Battery = battery;
            CurrentCoordinate = new Position();
        }

        public CleaningResult Run(CleaningRequest request)
        {
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
                CurrentFacing = (Facing)Enum.Parse(typeof(Facing), facingActionResult[(int)CurrentFacing][(int)CurrentRobotAction]);
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