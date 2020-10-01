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
