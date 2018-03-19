using System.Collections.Concurrent;
using System.Collections.Generic;
//using System.Diagnostics.Contracts;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public class DefaultDriversStatusStorage : IDriversStatusStorage
    {
        public DefaultDriversStatusStorage() {
            this.statuses = new ConcurrentDictionary<
                                    DriverModuleName, DriverStatus>();
        }

        public IReadOnlyDictionary<DriverModuleName, DriverStatus>
               Statuses => this.statuses;

        public void Save(DriverModuleName moduleName, DriverStatus status) {
            //Contract.Requires(moduleName != null && status != null);

            this.statuses[moduleName] = status;
        }


        private readonly ConcurrentDictionary<DriverModuleName, DriverStatus>
                         statuses;
    }
}