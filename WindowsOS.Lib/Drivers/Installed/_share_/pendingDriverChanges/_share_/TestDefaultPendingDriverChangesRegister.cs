namespace WindowsOS.Lib.Drivers.Installed
{
    public class TestDefaultPendingDriverChangesRegister :
                                        DefaultPendingDriverChangesRegister,
                                        ITestPendingDriverChangesRegister
    {
        public TestDefaultPendingDriverChangesRegister() { }

        public void OnWindowsOsRestart() {
            base.rwLock.EnterWriteLock();
            try {
                base.activation.Clear();
                base.deactivation.Clear();
            }
            finally {
                base.rwLock.ExitWriteLock();
            }
        }
    }
}