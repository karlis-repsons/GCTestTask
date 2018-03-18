using System.Diagnostics;

namespace WindowsOS.Lib
{
    public class WindowsOsRunStateController : IWindowsOsRunStateController
    {
        public void Reboot() {
            Process.Start("cmd.exe /c shutdown", "-rf");
        }
    }
}