using System;
using System.Collections.Generic;
//using System.Diagnostics.Contracts;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public class DefaultDriversInfoFetcher : IDriversInfoFetcher
    {
        public DefaultDriversInfoFetcher(
                                            IDriverQueries driverQueryable,
                                            IDriversInfoStorage infoStorage
        ){
            //Contract.Requires(driverQueryable != null && statusStorage != null);

            this.driverQueryable = driverQueryable;
            this.infoStorage = infoStorage;
        }

        public void Fetch() {
            this.infoStorage.Save(
                                    this.driverQueryable.GetAllDriversInfo()
            );

            this.FetchDone?.Invoke(this, null);
        }

        public event EventHandler FetchDone;


        private readonly IDriverQueries driverQueryable;
        private readonly IDriversInfoStorage infoStorage;
    }
}