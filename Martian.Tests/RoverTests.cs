using System;
using Xunit;
using Martian;
using static Martian.Command;
using static Martian.Direction;
using System.Collections;
using System.Collections.Generic;

namespace Martian.Tests
{
    public class RoverTests
    {
        [Fact]
        public void RoverExecuteCommandsCorrectly()
        {
            Field field = new Field(new Point(5, 5));
            Rover rover = new Rover(field, new Point(2, 2), NORTH);
            List<Command> commands = new List<Command>() { FORWARD, TURN_LEFT, FORWARD };

            rover.ExecuteCommands(commands);

            Assert.Equal("1 3 W", rover.ToString());
        }

        [Fact]
        public void RoverCheckingAllRudderDirections()
        {
            Field field = new Field(new Point(6, 6));
            Rover rover = new Rover(field, new Point(3, 1), NORTH);
            List<Command> commands = new List<Command>()
            {
                FORWARD, TURN_RIGHT, FORWARD, TURN_LEFT, FORWARD    // (4, 3)
                ,TURN_LEFT, FORWARD, TURN_RIGHT, FORWARD            // (3, 4)
                ,TURN_LEFT, FORWARD, TURN_RIGHT, FORWARD            // (2, 5)
                ,TURN_LEFT, FORWARD, TURN_LEFT, FORWARD             // (1, 4)
                ,TURN_RIGHT, FORWARD, TURN_LEFT, FORWARD            // (0, 3)
            };

            rover.ExecuteCommands(commands);
            
            Assert.Equal("0 3 S", rover.ToString());
        }

        [Fact]
        public void RoverLostWhenCrossTheEdge()
        {
            Field field = new Field(new Point(5, 5));
            Rover rover = new Rover(field, new Point(2, 3), NORTH);
            List<Command> commands = new List<Command>() { FORWARD, FORWARD, FORWARD };

            rover.ExecuteCommands(commands);

            Assert.True(rover.lost);
            Assert.Equal("2 5 N LOST", rover.ToString());
        }

        [Fact]
        public void PointBecomesProtectedPointsWhenRoverLost()
        {
            Field field = new Field(new Point(5, 5));
            Rover rover = new Rover(field, new Point(2, 3), NORTH);
            List<Command> commands = new List<Command>() { FORWARD, FORWARD, FORWARD };

            rover.ExecuteCommands(commands);

            Assert.Contains(rover.currPosition, field.protectedPoints);
        }

        [Fact]
        public void RoverNotLostWhenPointProtected()
        {
            Field field = new Field(new Point(5, 5));
            field.protectedPoints.Add(new Point(2, 5));
            Rover rover = new Rover(field, new Point(2, 3), NORTH);

            List<Command> commands = new List<Command>() { FORWARD, FORWARD, FORWARD };

            rover.ExecuteCommands(commands);

            Assert.False(rover.lost);
            Assert.Equal("2 5 N", rover.ToString());
        }
    }
}
