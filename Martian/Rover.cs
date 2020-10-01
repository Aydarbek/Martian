using System.Collections;
using System.Collections.Generic;

namespace Martian
{
    public class Rover
    {
        public Point position { get; set; }
        public Direction direction { get; set; }
        public bool lost { get; set; } = false;
        private MarsSpace field;
        internal IRudderState rudderState { get; set; }
        

        internal NorthDirection northDirection;
        internal WestDirection westDirection;
        internal EastDirection eastDirection;
        internal SouthDirection southDirection;


        public Rover(MarsSpace field, Point startPoint, Direction startDirection)
        {
            this.field = field;
            position = startPoint;
            direction = startDirection;
            CreateRudderStates();
            SetRudderState();
        }

        private void CreateRudderStates()
        {
            northDirection = new NorthDirection(this);
            westDirection = new WestDirection(this);
            eastDirection = new EastDirection(this);
            southDirection = new SouthDirection(this);
        }

        private void SetRudderState()
        {
            switch (direction)
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
            foreach (Command command in commands)
                ExecuteCommand(command);
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

        public override string ToString()
        {
            string result = position.x + " " + position.y + " " + (char)direction;
            if (lost)
                result += " LOST";

            return result;
        }
    }    
}
