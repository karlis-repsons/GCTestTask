using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    /// <summary>
    /// Provides drivers data required by DriversProxies.Default
    /// testable drivers proxy. Simplified and thread-safe.
    /// 
    /// More elaborate DriversProxy would match a more elaborate
    /// test drivers source with deeper API (not just list and dictionary).
    /// </summary>
    public interface ITestDriversSource
    {
        List<Dictionary<DriverPropName, string>>
            GetPropDictionariesCopy();

        Dictionary<DriverModuleName, DriverStatus>
            GetStatusDictionaryCopy();

        void SetPropDictionaries(
                                    List<Dictionary<DriverPropName, string>>
                                    propDictionaries
        );

        void SetStatusDictionary(
                                    Dictionary<DriverModuleName, DriverStatus> 
                                    statusDictionary
        );

        string GetProp(DriverModuleName moduleName, DriverPropName propName);

        void SetProp( DriverModuleName moduleName, DriverPropName propName,
                      string value                                            );

        DriverStatus GetStatus(DriverModuleName moduleName);

        void SetStatus(DriverModuleName moduleName, DriverStatus status);

        void SetDefaults();
    }
}