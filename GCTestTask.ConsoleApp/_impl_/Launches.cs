using System;
using System.Collections.Generic;

using WindowsOS.Lib;
using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;
using WindowsOS.Lib.Drivers.Installed.DriversProxies.Default;
using GCTestTask.Lib;

namespace GCTestTask.ConsoleApp
{
    public class Launches
    {
        public Launches() {
            this.SetupDependencyInjection();
            this.LaunchEnablementSwitcher();
            this.LaunchFollower();
        }

        private void SetupDependencyInjection () {
            // TODO - use some DI tech
            this.pendingChanges = new DefaultPendingDriverChangesRegister();
            this.proxy = new DriversProxy(pendingChanges);
        }

        private void LaunchEnablementSwitcher() {
            this.switcher = new DriversScheduledEnablementSwitcher(
                                    this.proxy.Query,
                                    this.proxy.Manage,
                                    this.pendingChanges
            );

            string moduleName = GCTaskFilesInterface.GetNameOfModuleToDisable();
            IEnumerable<DateTime> inputSchedule 
                = GCTaskFilesInterface.GetDisablementSchedule();

            if (inputSchedule == null || string.IsNullOrEmpty(moduleName))
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
            this.follower = new DriversStatusFollower(this.proxy.Query);
            this.follower.UpdateDone
                += (sender, ea) => this.OnUpdatedDriversInfo();

            this.follower.Follow();
        }

        private IReadOnlyCollection<GCTaskDriverInfo> MakeDriversInfo() {
            throw new NotImplementedException();
        }

        private void OnUpdatedDriversInfo() {
            Console.WriteLine("Update: started...");

            IReadOnlyDictionary<DriverModuleName, DriverStatus>
                driversStatuses = this.follower.Statuses;

            var driversInfo = new List<GCTaskDriverInfo>();
            foreach (DriverModuleName mn in driversStatuses.Keys) {
                try {
                    var info = new GCTaskDriverInfo {
                        ModuleName = mn.ToString(),
                        DisplayName = this.proxy.Query
                                          .GetDisplayName(mn).ToString(),
                        IsActivated = driversStatuses[mn].IsActivated,
                        SupportsDisabling = this.proxy.Query
                                                .SupportsDisabling(mn)
                    };

                    driversInfo.Add(info);
                }
                catch (DriverException e) {
                    Console.WriteLine(
                        "[error] exception while fetching data: " + e.Message);
                }
            }
            
            GCTaskFilesInterface.SaveStatuses(driversInfo);

            Console.WriteLine("Update: done.");
        }


        private DefaultPendingDriverChangesRegister pendingChanges;
        private DriversProxy proxy;
        private DriversStatusFollower follower;
        private DriversScheduledEnablementSwitcher switcher;
    }
}