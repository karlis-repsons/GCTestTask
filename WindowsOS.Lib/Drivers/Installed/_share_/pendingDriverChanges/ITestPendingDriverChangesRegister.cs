namespace WindowsOS.Lib.Drivers.Installed
{
    public interface ITestPendingDriverChangesRegister : 
                                        IPendingDriverChangesRegister
    {
        void OnWindowsOsRestart();
    }
}