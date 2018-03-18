using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class DriverQueryable : IDriverQueries
    {
        public DriverQueryable(IPendingDriverChanges pendingChanges) {
            this.driversLister = new AllDriversLister();
            this.driverPropsGetter = new AnyDriverPropsGetter();
            this.driverStatusGetter = new AnyDriverStatusGetter(pendingChanges);
        }

        public DriverQueryable(
                                IDriversList driversLister,
                                IDriverProps driverPropsGetter,
                                IDriverStatus driverStatusGetter
        ){
            this.driversLister = driversLister;
            this.driverPropsGetter = driverPropsGetter;
            this.driverStatusGetter = driverStatusGetter;
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


        private readonly IDriversList driversLister;
        private readonly IDriverProps driverPropsGetter;
        private readonly IDriverStatus driverStatusGetter;
    }
}