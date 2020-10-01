using Martian.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Martian
{
    public class InputDataHelper
    {
        string inputData;
        string[] inputLines;
        string validationMessage;
        CommandCenter commandCenter;


        internal InputDataHelper(CommandCenter commandCenter) 
        {
            this.commandCenter = commandCenter;
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
            return true;
        }

        private void SetUpCommandCenterData()
        {
            inputLines = inputData.Split('\n');
            commandCenter.fieldParams = GetFieldParams();
            commandCenter.robotParams = GetRobotParams();
        }

        private string GetFieldParams()
        {
            return inputLines[0];
        }

        private Dictionary<string, string> GetRobotParams()
        {
            Dictionary<string, string> resultParams = new Dictionary<string, string>();

            for (int i = 1; i < inputLines.Length - 1; i = i + 2)
            {
                string locationParam = inputLines[i];
                string commandParam = inputLines[i + 1];
                resultParams.Add(locationParam, commandParam);
            }

            if (!ValidateRobotParams(resultParams))
                throw new ValidationException(validationMessage);
                
            return resultParams;
        }

        private bool ValidateRobotParams(Dictionary<string, string> resultParams)
        {
            return true;
        }
    }
}