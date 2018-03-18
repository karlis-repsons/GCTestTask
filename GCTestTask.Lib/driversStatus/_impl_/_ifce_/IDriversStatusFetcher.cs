using System;

namespace GCTestTask.Lib
{
    public interface IDriversStatusFetcher
    {
        void Fetch();

        event Action FetchDone;
    }
}