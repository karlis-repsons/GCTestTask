using System.Collections.Generic;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public interface IDriversInfoStorage
    {
        IReadOnlyDictionary<DriverModuleName, DriverInfo> Info { get; }

        void Save(List<DriverInfo> driversInfoList);
    }
}