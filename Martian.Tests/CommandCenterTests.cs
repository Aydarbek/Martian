using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Martian;
using Martian.Exceptions;

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
                "03 W\n" +
                "LLFFFLFLFL";

            CommandCenter center = CommandCenter.GetInstance();
            InputDataHelper dataHelper = InputDataHelper.GetInstance();

            center.ProcessInputData(inputData);

            Assert.Equal("5 3", center.fieldParams);
            Assert.Equal("1 1 E", center.robotLocationParams[0]); Assert.Equal("3 2 N", center.robotLocationParams[1]);
            Assert.Equal("FRRFLLFFRRFLL", center.robotCommands[1]); Assert.Equal("LLFFFLFLFL", center.robotCommands[2]);
        }

        [Fact] 
        public void WrongDataInputRaisesException()
        {
            string inputData =
                "5 3\n" +
                "1 1 E\n" +
                "RFRFINRFRF";

            CommandCenter center = CommandCenter.GetInstance();
            InputDataHelper dataHelper = InputDataHelper.GetInstance();

            var ex = Assert.Throws<ValidationException>(() => center.ProcessInputData(inputData));
            Assert.Equal("Wrong command instructions. Only L, R, F commands allowed!", ex.Message);
        }

        [Fact]
        public void SetUpEnvironmentCreatesLocationField()
        {
            CommandCenter center = CommandCenter.GetInstance();
            center.fieldParams = "5 3";

            center.SetupLocationField();

            Assert.NotNull(center.martianField);
            Assert.Equal(5, center.martianField.width);
        }

        [Fact]
        public void DropOffRobotsStoresRobotsToList()
        {
            CommandCenter center = CommandCenter.GetInstance();
            center.robotLocationParams = new List<string> { "1 1 E", "3 2 N", "0 3 W" };

            center.DropOffRobots();

            Assert.Equal(3, center.martianRobots.Count);
            Assert.Equal(Direction.NORTH, center.martianRobots[1].currDirection);
        }

        [Fact]
        public void ExecuteRobotCommands()
        {
            CommandCenter center = CommandCenter.GetInstance();
            Field field = new Field(new Point(5, 3));
            center.martianRobots = new List<Robot>
            {
                new Robot(field, new Point(1, 1), Direction.EAST),
                new Robot(field, new Point(3, 2), Direction.NORTH),
                new Robot(field, new Point(0, 3), Direction.WEST)
            };
            center.robotCommands = new List<string> { "RFRFRFRF", "FRRFLLFFRRFLL", "LLFFFLFLFL" };

            center.ExecuteRobotCommands();

            Assert.Equal("1 1 E", center.operationResults[0]);
            Assert.Equal("3 3 N LOST", center.operationResults[1]);
        }

        [Fact]
        public void PrintOperationReportBuildsCorrectOutput()
        {
            CommandCenter center = CommandCenter.GetInstance();
            center.operationResults = new List<string> { "1 1 E", "3 3 N LOST", "2 3 S" };

            center.PrintOperationReport();

            Assert.Equal("1 1 E\n3 3 N LOST\n2 3 S", center.operationReport);
        }


    }
}
