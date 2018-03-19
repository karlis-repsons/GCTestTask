using System;
using System.Collections.Generic;
//using System.Diagnostics.Contracts;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public class DefaultDriversStatusFetcher : IDriversStatusFetcher
    {
        public DefaultDriversStatusFetcher(
                                            IDriverQueries driverQueryable,
                                            IDriversStatusStorage statusStorage
        ){
            //Contract.Requires(driverQueryable != null && statusStorage != null);

            this.driverQueryable = driverQueryable;
            this.statusStorage = statusStorage;
        }

        public void Fetch() {
            HashSet<DriverModuleName> driverModuleNames
                                        = this.driverQueryable.GetModuleNames();

            foreach (DriverModuleName mn in driverModuleNames) {
                var q = this.driverQueryable;
                var status = new DriverStatus {
                    IsActivated = q.IsActivated(mn),
                    IsActivationPending = q.IsActivationPending(mn),
                    IsDeactivationPending = q.IsDeactivationPending(mn)
                };

                this.statusStorage.Save(mn, status);
            }

            this.FetchDone?.Invoke(this, null);
        }

        public event EventHandler FetchDone;


        private readonly IDriverQueries driverQueryable;
        private readonly IDriversStatusStorage statusStorage;
    }
}