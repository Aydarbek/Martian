using Martian.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class CommandCenter
    {
        internal string fieldParams { get; set; }
        internal Dictionary<string, string> robotParams { get; set; } = new Dictionary<string, string>();
        internal List<string> operationResults { get; set; } = new List<string>();
        internal string operationReport { get; set; }
        internal Field martianField { get; private set; }
        internal List<Robot> martianRobots { get; set; } = new List<Robot>();
        internal InputDataHelper dataHelper;

        internal static CommandCenter commandCenter;

        internal CommandCenter() 
        {
            dataHelper = new InputDataHelper(this); 
        }

        public static CommandCenter GetInstance()
        {
            if (commandCenter == null)
                commandCenter = new CommandCenter();

            return commandCenter;
        }

        public void DeliverMarsOperations(string inputData)
        {
            try
            {
                ProcessInputData(inputData);
                SetupLocationField();
                DropOffRobots();
                ExecuteRobotCommands();
                PrintOperationReport();
            }
            catch(ValidationException ex)
            {
                Console.WriteLine("Wrong data format!\n Validation message: " + ex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        internal void ProcessInputData(string inputData)
        {
            dataHelper.ProcessInputData(inputData);
        }

        internal void SetupLocationField()
        {
            martianField = Field.GetField(fieldParams);
        }

        internal void DropOffRobots()
        {
            foreach (string locationParams in robotParams.Keys)
                martianRobots.Add(Robot.GetRobot(martianField, locationParams));
        }

        internal void ExecuteRobotCommands()
        {
            foreach (Robot robot in martianRobots)
            {
                robot.ExecuteCommands(robotParams[robot.ToString()]);
                operationResults.Add(robot.ToString());
            }
        }


        internal void PrintOperationReport()
        {
            StringBuilder result = new StringBuilder();
            foreach (string resultItem in operationResults)
                result.AppendLine(resultItem);

            operationReport = result.ToString();

            Console.WriteLine(operationReport);
        }

    }
}
