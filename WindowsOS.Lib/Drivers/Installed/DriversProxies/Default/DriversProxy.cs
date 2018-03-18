namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class DriversProxy : IDriversProxy
    {
        public DriversProxy(
                            IPendingDriverChangesRegister pendingChangesRegister
        ){
            this.queryable = new DriverQueryable(pendingChangesRegister);
            this.manager = new DriverManager(   new AnyDriverPropsGetter(), 
                                                pendingChangesRegister        );
        }

        public DriversProxy(
                                IDriverQueries queryable,
                                IDriverManagement manager
        ){
            this.queryable = queryable;
            this.manager = manager;
        }

        public IDriverQueries Query => this.queryable;

        public IDriverManagement Manage => this.manager;


        private readonly IDriverQueries queryable;
        private readonly IDriverManagement manager;
    }
}