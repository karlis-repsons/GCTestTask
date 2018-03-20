using System.Collections.Generic;
using System.Management;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class DriverQueryable : IDriverQueries
    {
        public DriverQueryable(IPendingDriverChanges pendingChanges) {
            this.pendingChanges = pendingChanges;
            this.driversLister = new AllDriversLister();
            this.driverPropsGetter = new AnyDriverPropsGetter();
            this.driverStatusGetter = new AnyDriverStatusGetter(pendingChanges);
        }

        public DriverQueryable(
                                IDriversList driversLister,
                                IDriverProps driverPropsGetter,
                                IDriverStatus driverStatusGetter,
                                IPendingDriverChanges pendingChanges
        ) {
            this.driversLister = driversLister;
            this.driverPropsGetter = driverPropsGetter;
            this.driverStatusGetter = driverStatusGetter;
            this.pendingChanges = pendingChanges;
        }

        public HashSet<DriverModuleName> GetModuleNames()
            => this.driversLister.GetModuleNames();

        public bool HasDriver(DriverModuleName n)
            => this.driversLister.HasDriver(n);

        public string GetDisplayName(DriverModuleName mn)
            => this.driverPropsGetter.GetDisplayName(mn);

        public bool SupportsDisabling(DriverModuleName n)
            => this.driverPropsGetter.SupportsDisabling(n);

        public bool RequiresRestartToWork(DriverModuleName n)
            => this.driverPropsGetter.RequiresRestartToWork(n);

        public bool IsActivated(DriverModuleName n)
            => this.driverStatusGetter.IsActivated(n);

        public bool IsDeactivationPending(DriverModuleName n)
            => this.driverStatusGetter.IsDeactivationPending(n);

        public bool IsActivationPending(DriverModuleName n)
            => this.driverStatusGetter.IsActivationPending(n);

        public List<DriverInfo> GetAllDriversInfo() {
            var infoList = new List<DriverInfo>();

            var query = new SelectQuery(
                "SELECT " +
                "       Name, DisplayName, AcceptStop, Started " +
                "FROM " +
                "       Win32_SystemDriver"
            );
            using (var searcher = new ManagementObjectSearcher(query))
                foreach (ManagementObject driverQr in searcher.Get()) {
                    var moduleName
                            = new DriverModuleName((string)driverQr["Name"]);
                    var props = new DriverProps {
                            DisplayName = (string)driverQr["DisplayName"],
                            SupportsDisabling = (bool)driverQr["AcceptStop"],
                            RequiresRestartToWork = true // TODO
                    };
                    var status = new DriverStatus {
                            IsActivated = (bool)driverQr["Started"],
                            IsActivationPending
                                = this.pendingChanges.Activation
                                                     .Contains(moduleName),
                            IsDeactivationPending
                                = this.pendingChanges.Deactivation
                                                     .Contains(moduleName)
                    };
                    var driverInfo = new DriverInfo {
                                            ModuleName = moduleName,
                                            Props = props,
                                            Status = status
                    };

                    infoList.Add(driverInfo);
                }

            return infoList;
        }


        private readonly IPendingDriverChanges pendingChanges;
        private readonly IDriversList driversLister;
        private readonly IDriverProps driverPropsGetter;
        private readonly IDriverStatus driverStatusGetter;
    }
}