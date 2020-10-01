using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class NorthDirection : IRudderState
    {
        Rover rover { get; }

        internal NorthDirection(Rover rover)
        {
            this.rover = rover;
        }

        public void MoveForward()
        {
            if (rover.currPosition.y == rover.field.height)
                rover.MakeEdgeMove();
            else
                rover.currPosition.y++;
        }

        public void TurnLeft()
        {
            rover.currDirection = Direction.WEST;
            rover.rudderState = rover.westDirection;
        }

        public void TurnRight()
        {
            rover.currDirection = Direction.EAST;
            rover.rudderState = rover.eastDirection;
        }
    }
}
