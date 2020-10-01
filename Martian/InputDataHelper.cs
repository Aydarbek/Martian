using Martian.Exceptions;
using System;

namespace Martian
{
    public class InputDataHelper
    {
        string inputData;
        string[] inputLines;
        string validationMessage;

        CommandCenter commandCenter => CommandCenter.GetInstance();

        internal static void ValidateInputData(string inputData)
        {
            throw new NotImplementedException();
        }

        internal static InputDataHelper GetInstance()
        {
            throw new NotImplementedException();
        }

        internal void ProcessInputData(string inputData)
        {
            this.inputData = inputData;

            if (ValidateInputData())
                SetUpCommandCenterData();

            else
                throw new ValidationException(validationMessage);
        }

        private bool ValidateInputData()
        {
            throw new NotImplementedException();
        }

        private void SetUpCommandCenterData()
        {
            commandCenter.fieldParams = GetFieldParams();
            commandCenter.robotLocationParams = GetLocationParams();
            commandCenter.robotCommands = GetRobotInstructions();
        }

        private string GetFieldParams()
        {
            throw new NotImplementedException();
        }

        private string[] GetLocationParams()
        {
            throw new NotImplementedException();
        }

        private string[] GetRobotInstructions()
        {
            throw new NotImplementedException();
        }
    }
}