using System;
using System.Collections.Generic;

using WindowsOS.Lib;
using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public class DriversScheduledEnablementSwitcher : IDisposable
    {
        public DriversScheduledEnablementSwitcher(
                IDriverStatus driverStatusGetter,
                IDriverEnablementController enablementController,
                IPendingDriverChanges pendingChanges,
                IWindowsOsRunStateController windowsOsRunStateController = null
        ){
            this.driverStatusGetter = driverStatusGetter;
            this.enablementController = enablementController;
            this.pendingChanges = pendingChanges;
            this.rebootSwitch = windowsOsRunStateController;
            this.switchInitiators
                    = new List<DefaultScheduledEnablementSwitchInitiator>();
        }

        public void Disable(
                                DriverModuleName moduleName,
                                DriverEnablementSchedule schedule
        ){
            var enablementSwitch
                    = new DefaultOneDriverEnablementSwitch(
                                                moduleName,
                                                this.driverStatusGetter,
                                                this.enablementController   );
            var switchInitiator
                    = new DefaultScheduledEnablementSwitchInitiator(
                                                schedule, enablementSwitch   );

            this.switchInitiators.Add(switchInitiator);

            enablementSwitch.ActivationRequested += this.ActivationRequested;
            enablementSwitch.DeactivationRequested
                     += this.DeactivationRequested;

            this.ActivationRequested += this.ConsiderRestartingComputer;
            this.DeactivationRequested += this.ConsiderRestartingComputer;

            switchInitiator.Start();
        }

        public bool IsComputerRestartRequired {
            get {
                if (this.pendingChanges.Activation.Count > 0)
                    return true;

                if (    this.pendingChanges.Deactivation.Count > 0
                     && this.ShouldRebootToDeactivateDriver
                )
                    return true;

                return false;
            }
        }

        public bool ShouldRebootToDeactivateDriver { get; set; } = false;

        public event Action<DriverModuleName> DeactivationRequested;

        public event Action<DriverModuleName> ActivationRequested;

        public void Dispose() {
            foreach (   DefaultScheduledEnablementSwitchInitiator 
                        initiator in this.switchInitiators
            )
                initiator.Dispose();
        }


        private void ConsiderRestartingComputer(
                            DriverModuleName changedStatusModuleName
        ){
            if (this.IsComputerRestartRequired && this.rebootSwitch != null)
                this.rebootSwitch.Reboot();
        }

        private readonly IDriverStatus driverStatusGetter;
        private readonly IDriverEnablementController enablementController;
        private readonly IPendingDriverChanges pendingChanges;
        private readonly IWindowsOsRunStateController rebootSwitch;
        private readonly List<DefaultScheduledEnablementSwitchInitiator>
                                switchInitiators;
    }
}