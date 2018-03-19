using System;
using System.Collections.Generic;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public sealed class DriversStatusFollower : IDisposable
    {
        public DriversStatusFollower(IDriverQueries driverQueryable) {
            this.driverQueryable = driverQueryable;

            this.storage = new DefaultDriversStatusStorage();
            this.fetcher = new DefaultDriversStatusFetcher(
                                            driverQueryable, this.storage   );
            this.updater
                    = new DefaultDriversStatusPeriodicUpdater(this.fetcher);

            this.updater.UpdateDone 
                += (sender, ea) 
                        => this.UpdateDone?.Invoke(sender, ea);
        }

        public void Follow(uint updatePeriodMs = 10_000)
            => this.updater.StartPeriodicUpdates(updatePeriodMs);

        public event EventHandler UpdateDone;

        public IReadOnlyDictionary<DriverModuleName, DriverStatus>
               Statuses
        { get { return this.storage.Statuses; } }

        public void Dispose() => this.updater.Dispose();


        private readonly IDriverQueries driverQueryable;
        private readonly IDriversStatusStorage storage;
        private readonly IDriversStatusFetcher fetcher;
        private readonly DefaultDriversStatusPeriodicUpdater updater;
    }
}