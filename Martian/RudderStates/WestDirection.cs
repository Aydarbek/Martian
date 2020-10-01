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
            if (rover.currPosition.x == 0)
                rover.MakeEdgeMove();
            else
                rover.currPosition.x--;
        }

        public void TurnLeft()
        {
            rover.currDirection = Direction.SOUTH;
            rover.rudderState = rover.southDirection;
        }

        public void TurnRight()
        {
            rover.currDirection = Direction.NORTH;
            rover.rudderState = rover.northDirection;
        }
    }
}
