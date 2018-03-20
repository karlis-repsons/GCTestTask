using System;
using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    using DPNs = TestDriverPropNames;

    public class TestDriverQueriesBase
    {
        public TestDriverQueriesBase(ITestDriversSource s) {
            this.driversSource = s;
        }

        protected string GetPropString(
                                        DriverModuleName moduleName,
                                        DriverPropName propName
        ){
            if (moduleName == null)
                throw new InvalidDriverModuleName("null");
            if (propName == null)
                throw new InvalidDriverPropName("null");

            List<Dictionary<DriverPropName, string>> allDriversPropDicts
                = this.driversSource.GetPropDictionariesCopy();

            foreach (Dictionary<DriverPropName, string>
                     driverPropDict in allDriversPropDicts
            )
                if (driverPropDict[DPNs.ModuleName] == moduleName.ToString())
                    return driverPropDict[propName];

            throw new InvalidOperationException(
                string.Format("Could not get prop string for driver "
                              + "'{0}' prop '{1}'", moduleName, propName));
        }


        private readonly ITestDriversSource driversSource;
    }
}