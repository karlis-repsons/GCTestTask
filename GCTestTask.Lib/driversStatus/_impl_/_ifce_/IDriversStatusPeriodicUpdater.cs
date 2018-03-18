using System;

namespace GCTestTask.Lib
{
    public interface IDriversStatusPeriodicUpdater
    {
        void StartPeriodicUpdates(uint periodMs)
            // Not update when start method is called, but wait timeout.
        ;

        void StopPeriodicUpdates();

        event Action UpdateDone;
    }
}