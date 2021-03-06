using System;
using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class DriverEnablementController : IDriverEnablementController
    {
        public DriverEnablementController(
                        IDriverProps driverPropsGetter,
                        IPendingDriverChangesRegister pendingChangesRegister
        ) {
            this.driverPropsGetter = driverPropsGetter;
            this.pendingChanges = pendingChangesRegister;
        }

        public void EnableDriver(DriverModuleName moduleName) {
            if (this.pendingChanges != null)
                this.EnableDriverPatched(moduleName);
            else
                this.EnableDriverPure(moduleName);
        }

        public void DisableDriver(DriverModuleName moduleName) {
            if (this.pendingChanges != null)
                this.DisableDriverPatched(moduleName);
            else
                this.DisableDriverPure(moduleName);
        }


        private void EnableDriverPure(DriverModuleName moduleName) {
            // TODO: implement
            throw new NotImplementedException();
        }

        private void EnableDriverPatched(DriverModuleName moduleName) {
            if (moduleName == null)
                throw new InvalidDriverModuleName("null");

            // TODO: either get the real driver status and use it here,
            //       or get rid of this method and make related refactoring.

            if (    true // Because is better to say, enabled driver
                         // is waiting activation than is not, when it is
                         // already activated (how to get the real status?).
                 || this.driverPropsGetter.RequiresRestartToWork(moduleName)
            ){
                HashSet<DriverModuleName> driversWaitingActivation
                                            = this.pendingChanges.Activation;

                if (driversWaitingActivation.Contains(moduleName) == false) {
                    this.pendingChanges.OnEnableRequest(moduleName);
                    // TODO: implement driver enabler
                }
            }
        }

        private void DisableDriverPure(DriverModuleName moduleName) {
            // TODO: implement
            throw new NotImplementedException();
        }

        private void DisableDriverPatched(DriverModuleName moduleName) {
            if (moduleName == null)
                throw new InvalidDriverModuleName("null");

            if (this.driverPropsGetter.SupportsDisabling(moduleName) == false)
                throw new CannotDisableDriver(moduleName.ToString());

            // TODO: either get the real driver status and use it here,
            //       or get rid of this method and make related refactoring.

            // Assumption: if driver was loaded, it will never be unloaded
            //             by Windows kernel before computer restart.

            HashSet<DriverModuleName> driversWaitingDeactivation
                                        = this.pendingChanges.Deactivation;

            if (driversWaitingDeactivation.Contains(moduleName) == false) {
                this.pendingChanges.OnDisableRequest(moduleName);
                // TODO: implement driver disabler
            }
        }

        private readonly IDriverProps driverPropsGetter;
        private readonly IPendingDriverChangesRegister pendingChanges;
    }
}