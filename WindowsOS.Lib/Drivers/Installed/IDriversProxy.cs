namespace WindowsOS.Lib.Drivers.Installed
{
    /// <summary>
    /// Query and manage the installed Windows OS drivers.
    /// </summary>
    public interface IDriversProxy
    {
        IDriverQueries Query { get; }

        IDriverManagement Manage { get; }
    }
}