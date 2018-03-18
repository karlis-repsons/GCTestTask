namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class AnyDriverPropsGetter :
                                        DriverQueriesBase,
                                        IDriverProps
    {
        public string GetDisplayName(DriverModuleName moduleName)
            => GetPropString(moduleName, "DisplayName");

        public bool SupportsDisabling(DriverModuleName moduleName) {
            // TODO: implement logic, which gives always correct value.
            return bool.Parse(GetPropString(moduleName, "AcceptStop"));
        }

        public bool RequiresRestartToWork(DriverModuleName moduleName) {
            // TODO: implement logic, which gives always correct value.
            return true;
        }
    }
}