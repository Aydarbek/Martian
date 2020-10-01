using System;
using System.Collections.Generic;
using System.Text;

namespace Martian.RudderStates
{
    public interface IRudderExtendedState : IRudderState
    {
        void Backward();
        void TurnAround();
    }
}
