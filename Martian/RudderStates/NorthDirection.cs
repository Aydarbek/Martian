using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class NorthDirection : IRudderState
    {
        Robot robot { get; }

        internal NorthDirection(Robot robot)
        {
            this.robot = robot;
        }

        public void MoveForward()
        {
            if (robot.currPosition.y == robot.field.height)
                robot.MakeEdgeMove();
            else
                robot.currPosition.y++;
        }

        public void TurnLeft()
        {
            robot.currDirection = Direction.WEST;
            robot.rudderState = robot.westDirection;
        }

        public void TurnRight()
        {
            robot.currDirection = Direction.EAST;
            robot.rudderState = robot.eastDirection;
        }

    }
}
