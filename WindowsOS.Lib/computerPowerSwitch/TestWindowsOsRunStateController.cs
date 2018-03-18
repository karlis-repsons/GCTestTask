namespace WindowsOS.Lib
{
    public class TestWindowsOsRunStateController : IWindowsOsRunStateController
    {
        public void Reboot() => this.RebootCalled = true;

        public bool RebootCalled { get; private set; } = false;

        public void ClearTestParameters() {
            this.RebootCalled = false;
        }
    }
}