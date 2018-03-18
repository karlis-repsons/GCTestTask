namespace WindowsOS.Lib.Drivers.Installed
{
    public interface IDriverStatus
    {
        bool IsActivated(DriverModuleName moduleName);

        bool IsDeactivationPending(DriverModuleName moduleName);

        bool IsActivationPending(DriverModuleName moduleName);
    }
}