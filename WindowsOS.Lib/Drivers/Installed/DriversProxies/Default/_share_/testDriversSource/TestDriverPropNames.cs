namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    /// <summary>
    /// Use these with TestDriversSource.
    /// 
    /// For part of them, value strings correspond to 
    /// Win32_SystemDriver data column names.
    /// </summary>
    public static class TestDriverPropNames
    {
        public static readonly DriverPropName ModuleName
                        = new DriverPropName("Name");

        public static readonly DriverPropName DisplayName
                        = new DriverPropName("DisplayName");

        public static readonly DriverPropName SupportsDisabling
                        = new DriverPropName("AcceptStop");

        public static readonly DriverPropName RequiresRestartToWork
                        = new DriverPropName("RequiresRestartToWork");
    }
}