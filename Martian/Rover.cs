using Martian.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Martian
{
    public class Rover
    {
        public Point currPosition { get; set; }
        public Direction currDirection { get; set; }
        public bool lost { get; set; } = false;
        internal Field field;
        internal IRudderState rudderState { get; set; }        

        internal NorthDirection northDirection;
        internal WestDirection westDirection;
        internal EastDirection eastDirection;
        internal SouthDirection southDirection;


        public Rover(Field field, Point startPoint, Direction startDirection)
        {
            this.field = field;
            currPosition = startPoint;
            currDirection = startDirection;
            InitRudderStates();
            SetRudderState();
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
                case (Direction.NORTH): 
                    rudderState = northDirection;
                    break;
                case (Direction.EAST):
                    rudderState = eastDirection;
                    break;
                case (Direction.SOUTH):
                    rudderState = southDirection;
                    break;
                case (Direction.WEST):
                    rudderState = westDirection;
                    break;
                default:
                    rudderState = northDirection;
                    break;
            }
        }

        public void ExecuteCommands(IEnumerable<Command> commands)
        {
            try
            {
                foreach (Command command in commands)
                    ExecuteCommand(command);
            }
            catch (RoverLostException)
            {
                this.lost = true;
                field.protectedPoints.Add(currPosition);
            }
        }

        private void ExecuteCommand(Command command)
        {
            switch (command)
            {
                case (Command.FORWARD):
                    rudderState.MoveForward();
                    break;
                case (Command.TURN_LEFT):
                    rudderState.TurnLeft();
                    break;
                case (Command.TURN_RIGHT):
                    rudderState.TurnRight();
                    break;
            }
        }

        internal void MakeEdgeMove()
        {
            if (field.protectedPoints.Contains(currPosition))
                return;
            else
                throw new RoverLostException();
        }


        public override string ToString()
        {
            string result = currPosition.ToString() + " " + (char)currDirection;
            if (lost) result += " LOST";

            return result;
        }
    }    
}
