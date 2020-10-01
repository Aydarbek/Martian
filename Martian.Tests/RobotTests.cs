using System;
using Xunit;
using Martian;
using static Martian.Command;
using static Martian.Direction;
using System.Collections;
using System.Collections.Generic;

namespace Martian.Tests
{
    public class RobotTests
    {
        [Fact]
        public void RobotExecuteCommandsCorrectly()
        {
            Field field = new Field(new Point(5, 5));
            Robot robot = Robot.GetRobot(field, "2 2 N");
            List<Command> commands = new List<Command>() { FORWARD, TURN_LEFT, FORWARD };

            robot.ExecuteCommands(commands);

            Assert.Equal("1 3 W", robot.ToString());
        }

        [Fact]
        public void RobotExecuteCommandsFromStringCorrectly()
        {
            Field field = new Field(new Point(5, 3));
            Robot robot = Robot.GetRobot(field, "3 2 N");
            string commands = "FRRFLLFFRRFLL";

            robot.ExecuteCommands(commands);

            Assert.Equal("3 3 N LOST", robot.ToString());
        }

        [Fact]
        public void RobotCheckingAllRudderDirections()
        {
            Field field = new Field(new Point(6, 6));
            Robot robot = new Robot(field, new Point(4, 1), NORTH);
            List<Command> commands = new List<Command>()
            {
                FORWARD, TURN_RIGHT, FORWARD, TURN_LEFT, FORWARD    // (4, 3)
                ,TURN_LEFT, FORWARD, TURN_RIGHT, FORWARD            // (3, 4)
                ,TURN_LEFT, FORWARD, TURN_RIGHT, FORWARD            // (2, 5)
                ,TURN_LEFT, FORWARD, TURN_LEFT, FORWARD             // (1, 4)
                ,TURN_RIGHT, FORWARD, TURN_LEFT, FORWARD            // (0, 3)
            };

            robot.ExecuteCommands(commands);
            
            Assert.Equal("1 3 S", robot.ToString());
        }

        [Fact]
        public void RobotLostWhenCrossTheEdge()
        {
            Field field = new Field(new Point(5, 5));
            Robot robot = new Robot(field, new Point(2, 3), NORTH);
            List<Command> commands = new List<Command>() { FORWARD, FORWARD, FORWARD };

            robot.ExecuteCommands(commands);

            Assert.True(robot.lost);
            Assert.Equal("2 5 N LOST", robot.ToString());
        }

        [Fact]
        public void PointBecomesProtectedPointsWhenrobotLost()
        {
            Field field = new Field(new Point(5, 5));
            Robot robot = new Robot(field, new Point(2, 3), NORTH);
            List<Command> commands = new List<Command>() { FORWARD, FORWARD, FORWARD };

            robot.ExecuteCommands(commands);

            Assert.Contains(robot.currPosition, field.protectedPoints);
        }

        [Fact]
        public void RobotNotLostWhenPointProtected()
        {
            Field field = new Field(new Point(5, 5));
            field.protectedPoints.Add(new Point(2, 5));
            Robot robot = new Robot(field, new Point(2, 3), NORTH);

            List<Command> commands = new List<Command>() { FORWARD, FORWARD, FORWARD };

            robot.ExecuteCommands(commands);

            Assert.False(robot.lost);
            Assert.Equal("2 5 N", robot.ToString());
        }

    }
}
