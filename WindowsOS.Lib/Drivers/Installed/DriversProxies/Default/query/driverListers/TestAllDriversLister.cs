using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    using DPNs = TestDriverPropNames;

    public class TestAllDriversLister : IDriversList
    {
        public TestAllDriversLister(ITestDriversSource s) {
            this.driversSource = s;
        }

        public HashSet<DriverModuleName> GetModuleNames() {
            List<Dictionary<DriverPropName, string>> allDriversPropDicts 
                = this.driversSource.GetPropDictionariesCopy();

            var moduleNames = new HashSet<DriverModuleName>();
            foreach (Dictionary<DriverPropName, string>
                     driverPropDict in allDriversPropDicts
            )
                moduleNames.Add(
                    new DriverModuleName(driverPropDict[DPNs.ModuleName]));

            return moduleNames;
        }

        public bool HasDriver(DriverModuleName moduleName) {
            if (moduleName == null)
                throw new InvalidDriverModuleName("null");

            List<Dictionary<DriverPropName, string>> allDriversPropDicts
                = this.driversSource.GetPropDictionariesCopy();

            foreach (Dictionary<DriverPropName, string>
                     driverPropDict in allDriversPropDicts
            )
                if (driverPropDict[DPNs.ModuleName] == moduleName.ToString())
                    return true;

            return false;
        }


        private readonly ITestDriversSource driversSource;
    }
}