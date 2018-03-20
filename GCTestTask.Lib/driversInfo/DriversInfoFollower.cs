using System;
using System.Collections.Generic;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public sealed class DriversInfoFollower : IDisposable
    {
        public DriversInfoFollower(IDriverQueries driverQueryable) {
            this.driverQueryable = driverQueryable;

            this.storage = new DefaultDriversInfoStorage();
            this.fetcher = new DefaultDriversInfoFetcher(
                                            driverQueryable, this.storage   );
            this.updater
                    = new DefaultDriversInfoPeriodicUpdater(this.fetcher);

            this.updater.UpdateDone 
                += (sender, ea) 
                        => this.UpdateDone?.Invoke(sender, ea);
        }

        public void Follow(uint updatePeriodMs = 10_000)
            => this.updater.StartPeriodicUpdates(updatePeriodMs);

        public event EventHandler UpdateDone;

        public IReadOnlyDictionary<DriverModuleName, DriverInfo>
               Info
        { get { return this.storage.Info; } }

        public void Dispose() => this.updater.Dispose();


        private readonly IDriverQueries driverQueryable;
        private readonly IDriversInfoStorage storage;
        private readonly IDriversInfoFetcher fetcher;
        private readonly DefaultDriversInfoPeriodicUpdater updater;
    }
}