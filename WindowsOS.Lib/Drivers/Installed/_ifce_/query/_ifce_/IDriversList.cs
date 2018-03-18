using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed
{
    public interface IDriversList
    {
        HashSet<DriverModuleName> GetModuleNames();

        bool HasDriver(DriverModuleName moduleName);
    }
}