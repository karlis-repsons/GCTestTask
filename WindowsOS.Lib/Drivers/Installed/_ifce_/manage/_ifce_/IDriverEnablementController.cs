namespace WindowsOS.Lib.Drivers.Installed
{
    public interface IDriverEnablementController
    {
        void EnableDriver(DriverModuleName moduleName);

        void DisableDriver(DriverModuleName moduleName);
    }
}