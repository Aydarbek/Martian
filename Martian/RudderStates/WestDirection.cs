using System;

namespace Martian
{
    internal class WestDirection : IRudderState
    {
        Robot robot;

        internal WestDirection(Robot robot)
        {
            this.robot = robot;
        }

        public void MoveForward()
        {
            if (robot.currPosition.x == 0)
                robot.MakeEdgeMove();
            else
                robot.currPosition.x--;
        }

        public void TurnLeft()
        {
            robot.currDirection = Direction.SOUTH;
            robot.rudderState = robot.southDirection;
        }

        public void TurnRight()
        {
            robot.currDirection = Direction.NORTH;
            robot.rudderState = robot.northDirection;
        }

    }
}
