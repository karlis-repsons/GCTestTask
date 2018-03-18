namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class TestDriverManager : IDriverManagement
    {
        public TestDriverManager(
                       ITestDriversSource driversSource,
                       IDriverProps driverPropsGetter,
                       IPendingDriverChangesRegister pendingChangesRegister
        ){
            this.enablementController
                    = new TestDriverEnablementController(
                                                    driversSource, 
                                                    driverPropsGetter, 
                                                    pendingChangesRegister   );
        }

        public TestDriverManager(
                                IDriverEnablementController enablementController
        ){
            this.enablementController = enablementController;
        }

        public void EnableDriver(DriverModuleName moduleName)
            => this.enablementController.EnableDriver(moduleName);

        public void DisableDriver(DriverModuleName moduleName)
            => this.enablementController.DisableDriver(moduleName);


        private readonly IDriverEnablementController enablementController;
    }
}