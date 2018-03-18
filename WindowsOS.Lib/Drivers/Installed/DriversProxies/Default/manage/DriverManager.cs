namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class DriverManager : IDriverManagement
    {
        public DriverManager(
                        IDriverProps driverPropsGetter,
                        IPendingDriverChangesRegister pendingChangesRegister
        ) {
            this.enablementController
                    = new DriverEnablementController(
                                driverPropsGetter, pendingChangesRegister   );
        }

        public DriverManager(
                                IDriverEnablementController enablementController
        ) {
            this.enablementController = enablementController;
        }

        public void EnableDriver(DriverModuleName moduleName)
            => this.enablementController.EnableDriver(moduleName);

        public void DisableDriver(DriverModuleName moduleName)
            => this.enablementController.DisableDriver(moduleName);


        private readonly IDriverEnablementController enablementController;
    }
}