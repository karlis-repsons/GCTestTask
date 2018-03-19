//using System.Diagnostics.Contracts;
using System.Management;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class DriverQueriesBase
    {
        protected static string GetPropString(
                                        DriverModuleName moduleName,
                                        string propName
        ) {
            //Contract.Requires(
            //       string.IsNullOrEmpty(moduleName) == false
            //    && string.IsNullOrEmpty(propName) == false
            //);
            //Contract.Ensures(Contract.Result<string>() != null);

            string propString = string.Empty;

            var query = new SelectQuery(
                                string.Format(
                                        "SELECT {0} FROM Win32_SystemDriver "
                                      + "WHERE Name = '{1}'"
                                    , propName, moduleName.ToString())
            );

            using (var searcher = new ManagementObjectSearcher(query)) {
                try {
                    ManagementObjectCollection driverArray = searcher.Get();
                    if (driverArray.Count == 0)
                        throw new InvalidDriverModuleName(moduleName.ToString());

                    if (driverArray.Count == 1)
                        foreach (ManagementObject
                                 driverDictionary in driverArray
                        )
                            if (driverDictionary[propName] != null)
                                propString = (driverDictionary[propName]).ToString();

                    if (driverArray.Count > 1)
                        throw new TwoDriversWithSameModuleName(moduleName.ToString());
                } catch (ManagementException e) {
                    throw new InvalidDriverPropName(
                                "Check prop name: " + propName, e);
                }
            }

            return propString;
        }
    }
}