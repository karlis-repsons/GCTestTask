namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    using DPNs = TestDriverPropNames;

    public class TestAnyDriverPropsGetter :
                                            TestDriverQueriesBase,
                                            IDriverProps
    {
        public TestAnyDriverPropsGetter(ITestDriversSource s) : base(s) { }

        public string GetDisplayName(DriverModuleName moduleName)
            => base.GetPropString(moduleName, DPNs.DisplayName);

        public bool SupportsDisabling(DriverModuleName moduleName)
            => bool.Parse(base.GetPropString(
                                moduleName, DPNs.SupportsDisabling)   );

        public bool RequiresRestartToWork(DriverModuleName moduleName)
            => bool.Parse(base.GetPropString(
                                moduleName, DPNs.RequiresRestartToWork)   );
    }
}