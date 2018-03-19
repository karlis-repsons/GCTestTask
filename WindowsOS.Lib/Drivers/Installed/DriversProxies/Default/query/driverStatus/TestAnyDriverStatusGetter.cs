using System;
using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    public class TestAnyDriverStatusGetter :
                                            TestDriverQueriesBase,
                                            IDriverStatus
    {
        public TestAnyDriverStatusGetter(
                                            ITestDriversSource driversSource,
                                            IPendingDriverChanges pendingChanges
        ) : base(driversSource)
        {
            this.driversSource = driversSource;
            this.pendingChanges = pendingChanges;
        }

        public bool IsActivated(DriverModuleName n)
            => this.GetStatusFieldValue(n, s => s.IsActivated);

        public bool IsDeactivationPending(DriverModuleName n)
            => this.GetStatusFieldValue(n, s => s.IsDeactivationPending);

        public bool IsActivationPending(DriverModuleName n)
            => this.GetStatusFieldValue(n, s => s.IsActivationPending);


        private bool GetStatusFieldValue(
                                DriverModuleName moduleName, 
                                Func<DriverStatus, bool> selectValue
        ){
            Dictionary<DriverModuleName, DriverStatus> statusesCopy
                = this.driversSource.GetStatusDictionaryCopy();

            if (statusesCopy.ContainsKey(moduleName) == false)
                throw new InvalidDriverModuleName(moduleName.ToString());

            bool isActivated = selectValue(statusesCopy[moduleName]);
            return isActivated;
        }

        private readonly ITestDriversSource driversSource;
        private readonly IPendingDriverChanges pendingChanges;
    }
}