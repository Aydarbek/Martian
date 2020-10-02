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
        string[] splitParams;
        string fieldParams;
        string validationMessage;
        int fieldWidth;
        int fieldHeight;
        int locationX;
        int locationY;


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
            return ValidateFieldParamsFormat() && 
                   ValidateFieldSize();
        }

        private bool ValidateFieldParamsFormat()
        {
            if (!Regex.IsMatch(fieldParams, fieldPattern))
            {
                validationMessage = String.Format("Wrong format of field parameters string: \"{0}\".\n Example: \"5 3\"", fieldParams);

                return false;
            }

            return true;
        }

        private bool ValidateFieldSize()
        {
            ReadFieldParameters();

            if (fieldWidth > 50 || fieldHeight > 50)
            {
                validationMessage = String.Format("Field width ({0}) or height ({1}) cannot be more than 50", fieldWidth, fieldHeight);
                return false;
            }

            return true;
        }

        private void ReadFieldParameters()
        {
            splitParams = fieldParams.Split(' ');
            fieldWidth = Int32.Parse(splitParams[0]);
            fieldHeight = Int32.Parse(splitParams[1]);
        }

        private Dictionary<string, string> GetRobotParams()
        {
            Dictionary<string, string> resultParams = new Dictionary<string, string>();

            for (int i = 1; i < inputLines.Length - 1; i = i + 2)
                resultParams.Add(inputLines[i], inputLines[i + 1]);

            if (!ValidateRobotParams(resultParams))
                throw new ValidationException(validationMessage);
                
            return resultParams;
        }

        private bool ValidateRobotParams(Dictionary<string, string> resultParams)
        {
            return ValidateRobotLocationParams(resultParams) && 
                   ValidateRobotCommands(resultParams);
        }

        private bool ValidateRobotLocationParams(Dictionary<string, string> resultParams)
        {
            foreach (string locationParams in resultParams.Keys)
            {
                if (!Regex.IsMatch(locationParams, robotLocationPattern))
                {
                    validationMessage = String.Format
                        ("Wrong format of robot location parameters string: \"{0}\".\n Correct format: \"3 2 N\"", locationParams);
                    
                    return false;
                }

                splitParams = locationParams.Split(' ');
                locationX = Int32.Parse(splitParams[0]);
                locationY = Int32.Parse(splitParams[1]);

                if (locationX > fieldWidth ||
                    locationY > fieldHeight)
                {
                    validationMessage = String.Format
                        ("Error. Robot input location ({0} {1}) is outside field bounds ({2} {3}).",
                        locationX, locationY, fieldWidth, fieldHeight);
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
                    validationMessage = String.Format
                        ("Wrong command instructions format: \"{0}\".\n Only L, R, F commands allowed!", robotCommands);
                    return false;
                }

            return true;
        }

    }
}