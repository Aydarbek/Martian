using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public interface IRudderState
    {
        void MoveForward();
        void TurnRight();
        void TurnLeft();
    }
}
