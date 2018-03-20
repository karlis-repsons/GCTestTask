using System;

namespace GCTestTask.Lib
{
    public interface IDriversInfoPeriodicUpdater
    {
        void StartPeriodicUpdates(uint periodMs)
            // Not update when start method is called, but wait timeout.
        ;

        void StopPeriodicUpdates();

        event EventHandler UpdateDone;
    }
}