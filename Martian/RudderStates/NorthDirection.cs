using Martian.Engine;
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
            rover.direction = Direction.W;
            rover.rudderState = rover.westDirection;
        }

        public void TurnRight()
        {
            rover.direction = Direction.E;
            rover.rudderState = rover.eastDirection;
        }
    }
}
