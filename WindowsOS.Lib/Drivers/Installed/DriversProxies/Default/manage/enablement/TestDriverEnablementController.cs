using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class TestDriverEnablementController : IDriverEnablementController
    {
        public TestDriverEnablementController(
                                ITestDriversSource driversSource,
                                IDriverProps propsGetter,
                                IPendingDriverChangesRegister changesRegister
        ){
            this.driversSource = driversSource;
            this.driverPropsGetter = propsGetter;
            this.pendingChanges = changesRegister;
        }

        public void EnableDriver(DriverModuleName moduleName) {
            if (moduleName == null)
                throw new InvalidDriverModuleName("null");

            if (this.driversSource.GetStatus(moduleName).IsActivated)
                return;

            if (this.driverPropsGetter.RequiresRestartToWork(moduleName)) {
                HashSet<DriverModuleName> driversWaitingActivation
                                            = this.pendingChanges.Activation;

                if (driversWaitingActivation.Contains(moduleName))
                    return;

                this.pendingChanges.OnEnableRequest(moduleName);

                var s = new DriverStatus {
                                            IsActivated = false,
                                            IsActivationPending = true,
                                            IsDeactivationPending = false   };

                this.driversSource.SetStatus(moduleName, s);
            }
            else {
               var s = new DriverStatus {
                                            IsActivated = true,
                                            IsActivationPending = false,
                                            IsDeactivationPending = false   };

                this.driversSource.SetStatus(moduleName, s);
            }
        }

        public void DisableDriver(DriverModuleName moduleName) {
            if (moduleName == null)
                throw new InvalidDriverModuleName("null");

            if (this.driversSource.GetStatus(moduleName).IsActivated == false)
                return;

            if (this.driverPropsGetter.SupportsDisabling(moduleName) == false)
                throw new CannotDisableDriver(moduleName.ToString());

            // Assumption: if driver was loaded, it will never be unloaded
            //             by Windows kernel before computer restart.

            HashSet<DriverModuleName> driversWaitingDeactivation
                                        = this.pendingChanges.Deactivation;
            
            if (driversWaitingDeactivation.Contains(moduleName))
                return;

            this.pendingChanges.OnDisableRequest(moduleName);

            var s = new DriverStatus {
                                        IsActivated = true,
                                        IsActivationPending = false,
                                        IsDeactivationPending = true   };

            this.driversSource.SetStatus(moduleName, s);
        }

        private readonly ITestDriversSource driversSource;
        private readonly IDriverProps driverPropsGetter;
        private readonly IPendingDriverChangesRegister pendingChanges;
    }
}