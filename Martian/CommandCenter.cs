using Martian.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class CommandCenter
    {
        internal string fieldParams { get; set; }
        internal List<string> robotLocationParams { get; set; } = new List<string>();
        internal List<string> robotCommands { get; set; } = new List<string>();
        internal List<string> operationResults { get; set; } = new List<string>();
        internal string operationReport { get; set; }
        internal Field martianField { get; private set; }
        internal List<Robot> martianRobots { get; set; }
        internal InputDataHelper dataHelper => InputDataHelper.GetInstance();


        private static CommandCenter commandCenter;

        private CommandCenter() { }

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
                Console.WriteLine("Wrong data format. Validation message: " + ex.Message);
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
            throw new NotImplementedException();
        }

        internal void ExecuteRobotCommands()
        {
            throw new NotImplementedException();
        }


        internal void PrintOperationReport()
        {
            
            Console.WriteLine(operationReport);
        }

    }
}
