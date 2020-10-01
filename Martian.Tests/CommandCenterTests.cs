using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Martian;

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

            dataHelper.ProcessInputData(inputData);

            Assert.Equal("5 3", center.fieldParams);
            Assert.Equal("1 1 E", center.robotLocationParams[0]);
            Assert.Equal("LLFFFLFLFL", center.robotCommands[2]);
        }
    }
}
