using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    internal class SouthDirection : IRudderState
    {
        Rover rover;

        internal SouthDirection(Rover rover)
        {
            this.rover = rover;
        }

        public void MoveForward()
        {
            if (rover.currPosition.y == 0)
                rover.MakeEdgeMove();
            else
                rover.currPosition.y--;
        }

        public void TurnLeft()
        {
            rover.currDirection = Direction.EAST;
            rover.rudderState = rover.eastDirection;
        }

        public void TurnRight()
        {
            rover.currDirection = Direction.WEST;
            rover.rudderState = rover.westDirection;
        }
    }
}
