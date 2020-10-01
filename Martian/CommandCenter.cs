using Martian.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Martian
{
    public class CommandCenter
    {
        internal string fieldParams { get; set; }
        internal string[] robotLocationParams { get; set; }
        internal string[] robotCommands { get; set; }
        internal Field martianField { get; private set; }
        internal List<Robot> martianRobots { get; private set; }
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
                dataHelper.ProcessInputData(inputData);
                SetUpEnvironment();
                PlaceRobots();
                ExecuteRobotCommands();
            }

            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (Exception)
            {
                throw;
            }
        }


        public void SetUpEnvironment()
        {

        }

        private void ExecuteRobotCommands()
        {
            throw new NotImplementedException();
        }

        private void PlaceRobots()
        {
            throw new NotImplementedException();
        }



    }
}
