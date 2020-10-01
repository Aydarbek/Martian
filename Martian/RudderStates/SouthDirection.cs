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
            rover.position.y--;
        }

        public void TurnLeft()
        {
            rover.direction = Direction.EAST;
            rover.rudderState = rover.eastDirection;
        }

        public void TurnRight()
        {
            rover.direction = Direction.WEST;
            rover.rudderState = rover.westDirection;
        }
    }
}
