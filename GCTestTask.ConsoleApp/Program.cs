using System.Threading;

namespace GCTestTask.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args) {
            GCTaskConsoleInfoPrinter.PrintUsage();
            new Launches();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}