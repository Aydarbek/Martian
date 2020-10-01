using System;
using System.Collections;
using System.Collections.Generic;
using Martian.Exceptions;
using static Martian.Direction;
using static Martian.Command;


namespace Martian
{
    public class Robot
    {
        public Point currPosition { get; set; }
        public Direction currDirection { get; set; }
        public bool lost { get; set; } = false;
        internal IRudderState rudderState { get; set; }
        internal Field field;

        internal NorthDirection northDirection;
        internal WestDirection westDirection;
        internal EastDirection eastDirection;
        internal SouthDirection southDirection;


        public Robot(Field field, Point startPoint, Direction startDirection = NORTH)
        {
            this.field = field;
            currPosition = startPoint;
            currDirection = startDirection;
            InitRudderStates();
            SetRudderState();
        }

        public static Robot GetRobot(Field field, string initParams)
        {
            try
            {
                string[] locationParams = initParams.Split(' ');
                int x = Int32.Parse(locationParams[0]);
                int y = Int32.Parse(locationParams[1]);
                char direction = locationParams[2][0];

                return new Robot(field, new Point(x, y), (Direction)direction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitRudderStates()
        {
            northDirection = new NorthDirection(this);
            westDirection = new WestDirection(this);
            eastDirection = new EastDirection(this);
            southDirection = new SouthDirection(this);
        }

        private void SetRudderState()
        {
            switch (currDirection)
            {
                case (NORTH): 
                    rudderState = northDirection;
                    break;
                case (EAST):
                    rudderState = eastDirection;
                    break;
                case (SOUTH):
                    rudderState = southDirection;
                    break;
                case (WEST):
                    rudderState = westDirection;
                    break;
                default:
                    rudderState = northDirection;
                    break;
            }
        }

        public void ExecuteCommands(string commands)
        {
            try
            {
                foreach (char command in commands)
                    ExecuteCommand(command);
            }
            catch (RobotLostException)
            {
                MakeRescueMeasures();
            }
        }


        public void ExecuteCommands(IEnumerable<Command> commands)
        {
            try
            {
                foreach (Command command in commands)
                    ExecuteCommand((char)command);
            }
            catch (RobotLostException)
            {
                MakeRescueMeasures();
            }
        }

        private void MakeRescueMeasures()
        {
            this.lost = true;
            field.protectedPoints.Add(currPosition);
        }

        private void ExecuteCommand(char command)
        {
            switch ((Command)command)
            {
                case (FORWARD):
                    rudderState.MoveForward();
                    break;
                case (TURN_LEFT):
                    rudderState.TurnLeft();
                    break;
                case (TURN_RIGHT):
                    rudderState.TurnRight();
                    break;
            }
        }

        internal void MakeEdgeMove()
        {
            if (field.protectedPoints.Contains(currPosition))
                return;
            else
                throw new RobotLostException();
        }


        public override string ToString()
        {
            string result = currPosition.ToString() + " " + (char)currDirection;
            if (lost) result += " LOST";

            return result;
        }
    }    
}
