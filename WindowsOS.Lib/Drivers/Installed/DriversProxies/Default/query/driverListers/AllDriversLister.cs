using System.Collections.Generic;
using System.Management;
//using System.Diagnostics.Contracts;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class AllDriversLister : IDriversList
    {
        public HashSet<DriverModuleName> GetModuleNames() {
            //Contract.Ensures(Contract.Result<
            //                    HashSet<DriverModuleName>   >() != null);

            var moduleNames = new HashSet<DriverModuleName>();

            var query = new SelectQuery("SELECT Name FROM Win32_SystemDriver");
            using (var searcher = new ManagementObjectSearcher(query))
                foreach (ManagementObject driverInfo in searcher.Get())
                    moduleNames.Add(new DriverModuleName(
                                            (string)driverInfo["Name"]   ));

            return moduleNames;
        }

        public bool HasDriver(DriverModuleName moduleName) {
            //Contract.Requires(moduleName != null);

            bool hasDriver = false;

            var query = new SelectQuery(
                                "SELECT Name FROM Win32_SystemDriver "
                + string.Format("WHERE Name = '{0}'", moduleName)
            );

            using (var searcher = new ManagementObjectSearcher(query))
                if (searcher.Get().Count >= 1)
                    hasDriver = true;

            return hasDriver;
        }
    }
}