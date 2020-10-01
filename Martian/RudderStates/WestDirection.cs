using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }

        public void TurnLeft()
        {
            throw new NotImplementedException();
        }

        public void TurnRight()
        {
            throw new NotImplementedException();
        }
    }
}
