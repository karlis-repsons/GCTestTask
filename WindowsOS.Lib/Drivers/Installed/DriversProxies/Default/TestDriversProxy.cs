namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class TestDriversProxy : ITestDriversProxy
    {
        public TestDriversProxy(
                    ITestPendingDriverChangesRegister pendingChangesRegister,
                    ITestDriversSource driversSource = null
        ){
            if (driversSource == null)
                driversSource = new TestDriversSource();

            this.pendingChangesReg = pendingChangesRegister;
            this.driversSource = driversSource;

            this.queryable = new TestDriverQueryable(
                                    driversSource, pendingChangesRegister   );

            this.manager
                    = new TestDriverManager(
                            driversSource,
                            new TestAnyDriverPropsGetter(driversSource), 
                            pendingChangesRegister                         );
        }

        public TestDriversProxy(
                    ITestPendingDriverChangesRegister pendingChangesRegister,
                    IDriverQueries queryable,
                    IDriverManagement manager,
                    ITestDriversSource driversSource = null
        ) {
            if (driversSource == null)
                driversSource = new TestDriversSource();

            this.pendingChangesReg = pendingChangesRegister;
            this.driversSource = driversSource;

            this.queryable = queryable;
            this.manager = manager;
        }

        public IDriverQueries Query => this.queryable;

        public IDriverManagement Manage => this.manager;

        public void HandleWindowsOsRestart() {
            foreach (DriverModuleName driverWaitingActivation
                     in this.pendingChangesReg.Activation
            )
                this.driversSource
                    .SetStatus(   driverWaitingActivation,
                                  new DriverStatus {
                                            IsActivated = true,
                                            IsActivationPending = false,
                                            IsDeactivationPending = false   });

            foreach (DriverModuleName driverWaitingDeactivation
                     in this.pendingChangesReg.Deactivation
            )
                this.driversSource
                    .SetStatus(   driverWaitingDeactivation,
                                  new DriverStatus {
                                            IsActivated = false,
                                            IsActivationPending = false,
                                            IsDeactivationPending = false   });

            this.pendingChangesReg.OnWindowsOsRestart();
        }


        private readonly ITestPendingDriverChangesRegister pendingChangesReg;
        private readonly ITestDriversSource driversSource;
        private readonly IDriverQueries queryable;
        private readonly IDriverManagement manager;
    }
}