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
            rover.position.x--;
        }

        public void TurnLeft()
        {
            rover.direction = Direction.NORTH;
            rover.rudderState = rover.northDirection;
        }

        public void TurnRight()
        {
            rover.direction = Direction.SOUTH;
            rover.rudderState = rover.southDirection;
        }
    }
}
