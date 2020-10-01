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
