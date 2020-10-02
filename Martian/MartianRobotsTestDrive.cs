using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Martian.Tests")]
namespace Martian
{    
    class MartianRobotsTestDrive
    {
        static string inputData =
            "5 N\n" +
            "1 1 E\n" +
            "RFRFRFRF\n" +
            "3 2 N\n" +
            "FRRFLLFFRRFLL\n" +
            "0 3 W\n" +
            "LLFFFLFLFL";

        static void Main (string [] args)
        {
            CommandCenter commandCenter = new CommandCenter();
            commandCenter.DeliverMarsOperations(inputData);
        }
    }
}
