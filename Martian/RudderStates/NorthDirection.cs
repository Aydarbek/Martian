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
            rover.position.y++;
        }

        public void TurnLeft()
        {
            rover.direction = Direction.WEST;
            rover.rudderState = rover.westDirection;
        }

        public void TurnRight()
        {
            rover.direction = Direction.EAST;
            rover.rudderState = rover.eastDirection;
        }
    }
}
