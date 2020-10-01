using System;

namespace Martian
{
    internal class WestDirection : IRudderState
    {
        Rover rover;

        internal WestDirection(Rover rover)
        {
            this.rover = rover;
        }

        public void MoveForward()
        {
            rover.position.x--;
        }

        public void TurnLeft()
        {
            rover.direction = Direction.SOUTH;
            rover.rudderState = rover.southDirection;
        }

        public void TurnRight()
        {
            rover.direction = Direction.NORTH;
            rover.rudderState = rover.northDirection;
        }
    }
}
