using System;

namespace GCTestTask.Lib
{
    public interface IDriversInfoFetcher
    {
        void Fetch();

        event EventHandler FetchDone;
    }
}