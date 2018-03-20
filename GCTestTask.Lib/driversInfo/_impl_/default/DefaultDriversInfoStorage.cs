using System.Collections.Concurrent;
using System.Collections.Generic;
//using System.Diagnostics.Contracts;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public class DefaultDriversInfoStorage : IDriversInfoStorage
    {
        public DefaultDriversInfoStorage() {
            this.info = new ConcurrentDictionary<
                                        DriverModuleName, DriverInfo>();
        }

        public IReadOnlyDictionary<DriverModuleName, DriverInfo>
               Info => this.info;

        public void Save(List<DriverInfo> driversInfoList) {
            //Contract.Requires(moduleName != null && status != null);

            foreach (DriverInfo driverInfo in driversInfoList) {
                DriverModuleName moduleName = driverInfo.ModuleName;
                this.info[moduleName] = driverInfo;
            }
        }


        private readonly ConcurrentDictionary<DriverModuleName, DriverInfo>
                         info;
    }
}