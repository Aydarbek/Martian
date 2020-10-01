using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    internal class EastDirection : IRudderState
    {
        Rover rover;

        internal EastDirection(Rover rover)
        {
            this.rover = rover;
        }

        public void MoveForward()
        {
            if (rover.currPosition.y == rover.field.width)
                rover.MakeEdgeMove();
            else
                rover.currPosition.x++;
        }

        public void TurnLeft()
        {
            rover.currDirection = Direction.NORTH;
            rover.rudderState = rover.northDirection;
        }

        public void TurnRight()
        {
            rover.currDirection = Direction.SOUTH;
            rover.rudderState = rover.southDirection;
        }
    }
}
