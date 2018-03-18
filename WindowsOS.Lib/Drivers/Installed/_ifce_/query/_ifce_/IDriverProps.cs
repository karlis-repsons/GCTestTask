namespace WindowsOS.Lib.Drivers.Installed
{
    public interface IDriverProps
    {
        string GetDisplayName(DriverModuleName moduleName);

        bool SupportsDisabling(DriverModuleName moduleName);

        bool RequiresRestartToWork(DriverModuleName moduleName);
    }
}