using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    internal class SouthDirection : IRudderState
    {
        Robot robot;

        internal SouthDirection(Robot robot)
        {
            this.robot = robot;
        }

        public void MoveForward()
        {
            if (robot.currPosition.y == 0)
                robot.MakeEdgeMove();
            else
                robot.currPosition.y--;
        }

        public void TurnLeft()
        {
            robot.currDirection = Direction.EAST;
            robot.rudderState = robot.eastDirection;
        }

        public void TurnRight()
        {
            robot.currDirection = Direction.WEST;
            robot.rudderState = robot.westDirection;
        }

    }
}
