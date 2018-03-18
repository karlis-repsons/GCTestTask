using System.Collections.Generic;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public interface IDriversStatusStorage
    {
        IReadOnlyDictionary<DriverModuleName, DriverStatus> Statuses { get; }

        void Save(DriverModuleName moduleName, DriverStatus status);
    }
}