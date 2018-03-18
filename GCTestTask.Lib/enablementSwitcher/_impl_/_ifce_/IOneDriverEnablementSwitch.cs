using System;

using WindowsOS.Lib.Drivers;

namespace GCTestTask.Lib
{
    public interface IOneDriverEnablementSwitch
    {
        void RequestActivation();

        void RequestDeactivation();

        event Action<DriverModuleName> DeactivationRequested;

        event Action<DriverModuleName> ActivationRequested;
    }
}