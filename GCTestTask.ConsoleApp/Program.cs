using System;
using System.Threading;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;
using WindowsOS.Lib.Drivers.Installed.DriversProxies.Default;
using GCTestTask.Lib;

namespace GCTestTask.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args) {
            var pendingChanges = new DefaultPendingDriverChangesRegister();
            var proxy = new DriversProxy(pendingChanges);
            var follower = new DriversStatusFollower(proxy.Query);

            follower.UpdateDone += (sender, ea) => Console.WriteLine("update done.");
            follower.Follow(700);

            Thread.Sleep(Timeout.Infinite);
        }
    }
}