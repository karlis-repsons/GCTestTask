using System;

namespace GCTestTask.ConsoleApp
{
    public class GCTaskConsoleInfoPrinter
    {
        public static void PrintUsage() {
            string sep = "    ";
            Console.WriteLine(
                "Usage: \n" +
                "Interact with this program, by using 2 files at this path: \n" +
                sep + GCTaskFilesInterface.Path + "\n" +
                sep + sep + GCTaskFilesInterface.StatusesFileName + "\n" +
                sep + sep + GCTaskFilesInterface.ScheduleFileName + "\n" +
                "\n" +
                sep + sep + "Example schedule line: \"1394ohci 2018-03-15_17:10:00, 2018-03-17_18:10:00\"\n" +
                "\n" +
                "Further info about operation of program will be logged here.\n" +
                "------------------------------------------------------------\n" +
                "\n"
            );
        }
    }
}