using Martian.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Martian
{
    public class InputDataHelper
    {
        const string fieldPattern = "^\\d{1,2} \\d{1,2}$";
        const string robotLocationPattern = "^\\d{1,2} \\d{1,2} [N,W,E,S]$";
        const string robotCommandsPatern = "^[F,L,R]*$";
        
        CommandCenter commandCenter;

        string[] inputLines;
        string fieldParams;
        string validationMessage;
        int fieldWidth;
        int fieldHeight;


        internal InputDataHelper(CommandCenter commandCenter) 
        {
            this.commandCenter = commandCenter;
        }       

        internal void ProcessInputData(string inputData)
        {
            inputLines = inputData.Split('\n');
            
            if (ValidateInputData())
                SetUpCommandCenterData();
            else
                throw new ValidationException(validationMessage);
        }

        private bool ValidateInputData()
        {
            foreach(string line in inputLines)
                if (line.Length >= 100)
                {
                    validationMessage = "Input data string lenght must be less that 100";
                    return false;
                }

            return true;
        }

        private void SetUpCommandCenterData()
        {
            commandCenter.fieldParams = GetFieldParams();
            commandCenter.robotParams = GetRobotParams();
        }


        private string GetFieldParams()
        {
            fieldParams = inputLines[0];
            
            if (!ValidateFieldParams())
                throw new ValidationException(validationMessage);

            return fieldParams;
        }

        private bool ValidateFieldParams()
        {
            if(!Regex.IsMatch(fieldParams, fieldPattern)) 
            {
                validationMessage = "Wrong format of field parameters string. Example: \"5 3\"";
                return false;
            }

            string[] splitFieldParams = fieldParams.Split(' ');
            fieldWidth = Int32.Parse(splitFieldParams[0]);
            fieldHeight = Int32.Parse(splitFieldParams[1]);

            if (fieldWidth > 50 || fieldHeight > 50)
            {
                validationMessage = "Field width and height cannot be more than 50";
                return false;
            }

            return true;
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
            return ValidateRobotLocationParams(resultParams) && ValidateRobotCommands(resultParams);
        }

        private bool ValidateRobotLocationParams(Dictionary<string, string> resultParams)
        {
            int x;
            int y;
            string[] splitLocationParams;

            foreach (string locationParams in resultParams.Keys)
            {
                if (!Regex.IsMatch(locationParams, robotLocationPattern))
                {
                    validationMessage = "Wrong format of robot location parameters string. Correct format: \"3 2 N\"";
                    return false;
                }

                splitLocationParams = locationParams.Split(' ');
                x = Int32.Parse(splitLocationParams[0]);
                y = Int32.Parse(splitLocationParams[1]);

                if (x > fieldWidth || y > fieldHeight)
                {
                    validationMessage = "Error. Robot input location is outside field.";
                    return false;
                }
            }


            return true;
        }

        private bool ValidateRobotCommands(Dictionary<string, string> resultParams)
        {
            foreach (string robotCommands in resultParams.Values)
                if(!Regex. IsMatch(robotCommands, robotCommandsPatern))
                {
                    validationMessage = "Wrong command instructions format. Only L, R, F commands allowed!";
                    return false;
                }

            return true;
        }

    }
}