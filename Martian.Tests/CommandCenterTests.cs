using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Martian;
using Martian.Exceptions;
using System.Linq;

namespace Martian.Tests
{
    public class CommandCenterTests
    {
        [Fact]
        public void ProcessInputDataBindInstructionLinesVariables()
        {
            string inputData =
                "5 3\n" +
                "1 1 E\n" +
                "RFRFRFRF\n" +
                "3 2 N\n" +
                "FRRFLLFFRRFLL\n" +
                "0 3 W\n" +
                "LLFFFLFLFL";

            CommandCenter center = CommandCenter.GetInstance();

            center.ProcessInputData(inputData);

            Assert.Equal("5 3", center.fieldParams);
            Assert.Equal("1 1 E", center.robotParams.ElementAt(0).Key); 
            Assert.Equal("3 2 N", center.robotParams.ElementAt(1).Key);
            Assert.Equal("FRRFLLFFRRFLL", center.robotParams.ElementAt(1).Value); 
            Assert.Equal("LLFFFLFLFL", center.robotParams.ElementAt(2).Value);
        }

        [Fact] 
        public void WrongDataInputRaisesException()
        {
            string inputData =
                "5 3\n" +
                "1 1 E\n" +
                "RFRFINRFRF";

            CommandCenter center = new CommandCenter();

            var ex = Assert.Throws<ValidationException>(() => center.ProcessInputData(inputData));
            Assert.Equal("Wrong command instructions format: \"RFRFINRFRF\".\n Only L, R, F commands allowed!", ex.Message);
        }

        [Fact]
        public void SetUpEnvironmentCreatesLocationField()
        {
            CommandCenter center = new CommandCenter();
            center.fieldParams = "5 3";

            center.SetupLocationField();

            Assert.NotNull(center.martianField);
            Assert.Equal(5, center.martianField.width);
        }

        [Fact]
        public void DropOffRobotsStoresRobotsToList()
        {
            CommandCenter center = new CommandCenter();
            center.robotParams = new Dictionary<string, string>();
            center.robotParams.Add("1 1 E", "RFRFRFRF");
            center.robotParams.Add("3 2 N", "FRRFLLFFRRFLL");

            center.DropOffRobots();

            Assert.Equal(2, center.martianRobots.Count);
            Assert.Equal(Direction.NORTH, center.martianRobots[1].currDirection);
        }

        [Fact]
        public void ExecuteRobotCommands()
        {
            CommandCenter center = new CommandCenter();
            Field field = new Field(new Point(5, 3));
            center.martianRobots.Add(new Robot(field, new Point(1, 1), Direction.EAST));
            center.martianRobots.Add(new Robot(field, new Point(3, 2), Direction.NORTH));
            center.robotParams.Add("1 1 E", "RFRFRFRF");
            center.robotParams.Add("3 2 N", "FRRFLLFFRRFLL");

            center.ExecuteRobotCommands();

            Assert.Equal("1 1 E", center.operationResults[0]);
            Assert.Equal("3 3 N LOST", center.operationResults[1]);
        }

        [Fact]
        public void PrintOperationReportBuildsCorrectOutput()
        {
            CommandCenter center = new CommandCenter();
            center.operationResults = new List<string> { "1 1 E", "3 3 N LOST", "2 3 S" };

            center.PrintOperationReport();

            Assert.Equal("1 1 E\r\n3 3 N LOST\r\n2 3 S\r\n", center.operationReport);
        }


    }
}
