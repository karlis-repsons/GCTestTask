namespace WindowsOS.Lib.Drivers.Installed
{
    /// <summary>
    /// Simulate installed Windows OS drivers querying and management.
    /// Useful for testing functions, which use instances of IDriversProxy.
    /// </summary>
    public interface ITestDriversProxy : IDriversProxy
    {
        void HandleWindowsOsRestart();
    }
}