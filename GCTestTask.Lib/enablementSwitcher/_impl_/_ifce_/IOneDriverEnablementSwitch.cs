using System;

using WindowsOS.Lib.Drivers;

namespace GCTestTask.Lib
{
    public interface IOneDriverEnablementSwitch
    {
        void RequestActivation();

        void RequestDeactivation();

        event EventHandler<DriverModuleName> DeactivationRequested;

        event EventHandler<DriverModuleName> ActivationRequested;
    }
}