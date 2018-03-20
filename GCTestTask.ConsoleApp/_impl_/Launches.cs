using System;
using System.Collections.Generic;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;
using WindowsOS.Lib.Drivers.Installed.DriversProxies.Default;
using GCTestTask.Lib;

namespace GCTestTask.ConsoleApp
{
    public class Launches
    {
        public Launches() {
            this.pendingChanges = new DefaultPendingDriverChangesRegister();
            this.proxy = new DriversProxy(pendingChanges);

            this.LaunchEnablementSwitcher();
            this.LaunchFollower();
        }

        private void LaunchEnablementSwitcher() {
            DriverModuleName moduleName;
            {
                string mnStr = GCTaskFilesInterface.GetNameOfModuleToDisable();
                if (string.IsNullOrEmpty(mnStr)) {
                    Console.WriteLine("Could not get module name of driver to disable.");
                    return;
                }

                moduleName = new DriverModuleName(mnStr);
                if (this.proxy.Query.HasDriver(moduleName) == false) {
                    Console.WriteLine(string.Format(
                            "Driver {0} was not found.", mnStr));
                    return;
                }

                if (this.proxy.Query.SupportsDisabling(moduleName) == false) {
                    Console.WriteLine(string.Format(
                            "Driver {0} cannot be disabled.", mnStr   ));
                    return;
                }
            }

            this.switcher = new DriversScheduledEnablementSwitcher(
                                    this.proxy.Query,
                                    this.proxy.Manage,
                                    this.pendingChanges
            );

            IEnumerable<DateTime> inputSchedule 
                = GCTaskFilesInterface.GetDisablementSchedule();

            if (inputSchedule == null)
                return;

            this.switcher.DeactivationRequested
                    += (sender, ea)
                        => Console.WriteLine(string.Format(
                            "Deactivation requested for driver '{0}'.",
                            moduleName                                    ));

            var switcherSchedule = new DriverEnablementSchedule();
            foreach (DateTime time in inputSchedule)
                switcherSchedule[time] = DriverEnablementRequest.Deactivation;

            this.switcher.Disable(   new DriverModuleName(moduleName), 
                                     switcherSchedule                    );
        }

        private void LaunchFollower() {
            this.follower = new DriversInfoFollower(this.proxy.Query);
            this.follower.UpdateDone
                += (sender, ea) => this.OnUpdatedDriversInfo();

            this.follower.Follow();
        }

        private IReadOnlyCollection<DriverInfo> MakeDriversInfo() {
            throw new NotImplementedException();
        }

        private void OnUpdatedDriversInfo() {
            Console.WriteLine("Update started...");

            var driversInfo = new List<DriverInfo>();
            foreach (DriverInfo driverInfo in this.follower.Info.Values)
                    driversInfo.Add(driverInfo);
            
            GCTaskFilesInterface.SaveStatuses(driversInfo);

            Console.WriteLine("Update done.");
        }


        private DefaultPendingDriverChangesRegister pendingChanges;
        private DriversProxy proxy;
        private DriversInfoFollower follower;
        private DriversScheduledEnablementSwitcher switcher;
    }
}