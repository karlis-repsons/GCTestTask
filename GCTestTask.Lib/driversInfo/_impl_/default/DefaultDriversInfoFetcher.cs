using System;

using WindowsOS.Lib.Drivers.Installed;

namespace GCTestTask.Lib
{
    public class DefaultDriversInfoFetcher : IDriversInfoFetcher
    {
        public DefaultDriversInfoFetcher(
                                            IDriverQueries driverQueryable,
                                            IDriversInfoStorage infoStorage
        ){
            this.driverQueryable = driverQueryable
                                 ?? throw new ArgumentNullException();
            this.infoStorage = infoStorage
                                 ?? throw new ArgumentNullException();
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