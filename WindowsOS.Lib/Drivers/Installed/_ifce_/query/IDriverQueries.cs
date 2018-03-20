using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed
{
    public interface IDriverQueries : IDriversList, IDriverProps, IDriverStatus
    {
        List<DriverInfo> GetAllDriversInfo();
    }
}