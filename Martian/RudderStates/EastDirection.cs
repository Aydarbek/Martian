using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    internal class EastDirection : IRudderState
    {
        Robot robot;

        internal EastDirection(Robot robot)
        {
            this.robot = robot;
        }

        public void MoveForward()
        {
            if (robot.currPosition.y == robot.field.width)
                robot.MakeEdgeMove();
            else
                robot.currPosition.x++;
        }

        public void TurnLeft()
        {
            robot.currDirection = Direction.NORTH;
            robot.rudderState = robot.northDirection;
        }

        public void TurnRight()
        {
            robot.currDirection = Direction.SOUTH;
            robot.rudderState = robot.southDirection;
        }


        public void Backward()
        {
            throw new NotImplementedException();
        }


        public void TurnAround()
        {
            throw new NotImplementedException();
        }
    }
}
