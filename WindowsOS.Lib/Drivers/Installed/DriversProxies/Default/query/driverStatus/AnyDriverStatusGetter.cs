namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class AnyDriverStatusGetter :
                                        DriverQueriesBase,
                                        IDriverStatus
    {
        public AnyDriverStatusGetter(IPendingDriverChanges c) {
            this.pendingChanges = c;
        }

        public bool IsActivated(DriverModuleName moduleName) {
            // TODO: implement logic, which gives always correct value.

            bool isActivatedAccordingToWindows 
                    = bool.Parse(GetPropString(moduleName, "Started"));

            bool mayBeActivated 
                    =    isActivatedAccordingToWindows
                      || this.IsActivationPending(moduleName);

            return mayBeActivated;
        }

        public bool IsDeactivationPending(DriverModuleName n)
            => this.pendingChanges.Deactivation.Contains(n);

        public bool IsActivationPending(DriverModuleName n)
            => this.pendingChanges.Activation.Contains(n);


        private readonly IPendingDriverChanges pendingChanges;
    }
}