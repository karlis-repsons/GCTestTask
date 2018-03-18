using System;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public class DefaultOneDriverEnablementSwitch : IOneDriverEnablementSwitch
    {
        public DefaultOneDriverEnablementSwitch(
                                DriverModuleName moduleName,
                                IDriverStatus driverStatusGetter,
                                IDriverEnablementController enablementController
        ){
            this.moduleName = moduleName;
            this.driverStatusGetter = driverStatusGetter;
            this.enablementController = enablementController;
        }

        public void RequestActivation() {
            if (    this.driverStatusGetter.IsActivated(this.moduleName)
                 || this.driverStatusGetter.IsActivationPending(this.moduleName)
            )
                return;

            this.enablementController.EnableDriver(this.moduleName);
            this.ActivationRequested?.Invoke(this.moduleName);
        }

        public void RequestDeactivation() {
            if (
                this.driverStatusGetter.IsActivated(this.moduleName) == false
             || this.driverStatusGetter.IsDeactivationPending(this.moduleName)
            )
                return;

            this.enablementController.DisableDriver(this.moduleName);
            this.DeactivationRequested?.Invoke(this.moduleName);
        }

        public event Action<DriverModuleName> DeactivationRequested;

        public event Action<DriverModuleName> ActivationRequested;


        private readonly DriverModuleName moduleName;
        private readonly IDriverStatus driverStatusGetter;
        private readonly IDriverEnablementController enablementController;
    }
}