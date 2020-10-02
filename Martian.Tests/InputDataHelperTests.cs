using Martian.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Martian.Tests
{
    public class InputDataHelperTests
    {
        [Fact]
        public void LongStringInputRaisesException()
        {
            string inputData =
                "5 3\n" +
                "1 1 E\n" +
                "RFRFRFRF\n" +
                "3 2 N\n" +
                "FRRFLLFFRRFLLFRRFLLFFRRFLLFRRFLLFFRRFLLFRRFLLFFRRFLLFRRFLLFFRRFLLFRRFLLFFRRFLLFRRFLLFFRRFLLFRRFLLFFRRFLL\n" +
                "0 3 W\n" +
                "LLFFFLFLFL";

            InputDataHelper dataHelper = new InputDataHelper(new CommandCenter());

            Exception ex = Assert.Throws<ValidationException>(() => dataHelper.ProcessInputData(inputData));
            Assert.Equal("Input data string lenght must be less that 100", ex.Message);
        }

        [Fact]
        public void BigValuesOfFieldParametersRaisesException()
        {
            string inputData =
                "50 51\n" +
                "1 1 E\n" +
                "RFRFRFRF\n" +
                "3 2 N\n" +
                "FRRFLLFFRRFLL\n" +
                "0 3 W\n" +
                "LLFFFLFLFL";

            InputDataHelper dataHelper = new InputDataHelper(new CommandCenter());

            Exception ex = Assert.Throws<ValidationException>(() => dataHelper.ProcessInputData(inputData));
            Assert.Equal("Field width (50) or height (51) cannot be more than 50", ex.Message);
        }

        [Fact]
        public void WrongFieldParamsFormatRaisesException()
        {
            string inputData =
                "5 N\n" +
                "1 1 E\n" +
                "RFRFRFRF\n" +
                "3 2 N\n" +
                "FRRFLLFFRRFLL\n" +
                "0 3 W\n" +
                "LLFFFLFLFL";

            InputDataHelper dataHelper = new InputDataHelper(new CommandCenter());

            Exception ex = Assert.Throws<ValidationException>(() => dataHelper.ProcessInputData(inputData));
            Assert.Equal("Wrong format of field parameters string: \"5 N\".\n Example: \"5 3\"", ex.Message);
        }

        [Fact]
        public void WrongLocationParamsFormatRaisesException()
        {
            string inputData =
                "5 3\n" +
                "1 1 5\n" +
                "RFRFRFRF\n" +
                "3 2 N\n" +
                "FRRFLLFFRRFLL\n" +
                "0 3 W\n" +
                "LLFFFLFLFL";

            InputDataHelper dataHelper = new InputDataHelper(new CommandCenter());

            Exception ex = Assert.Throws<ValidationException>(() => dataHelper.ProcessInputData(inputData));
            Assert.Equal("Wrong format of robot location parameters string: \"1 1 5\".\n Correct format: \"3 2 N\"", ex.Message);
        }

        [Fact]
        public void WrongRobotCommandsFormatRaisesException()
        {
            string inputData =
                "5 3\n" +
                "1 1 E\n" +
                "RFRFRFRF\n" +
                "3 2 N\n" +
                "FRRFLLFFR786qRFLL\n" +
                "0 3 W\n" +
                "LLFFFLFLFL";

            InputDataHelper dataHelper = new InputDataHelper(new CommandCenter());

            Exception ex = Assert.Throws<ValidationException>(() => dataHelper.ProcessInputData(inputData));
            Assert.Equal("Wrong command instructions format: \"FRRFLLFFR786qRFLL\".\n Only L, R, F commands allowed!", ex.Message);
        }

        [Fact]
        public void RobotInputLocationOutsideFieldRaisesException()
        {
            string inputData =
                "5 3\n" +
                "10 1 E\n" +
                "RFRFRFRF\n";

            InputDataHelper dataHelper = new InputDataHelper(new CommandCenter());

            Exception ex = Assert.Throws<ValidationException>(() => dataHelper.ProcessInputData(inputData));
            Assert.Equal("Error. Robot input location (10 1) is outside field bounds (5 3).", ex.Message);
        }
    }
}
