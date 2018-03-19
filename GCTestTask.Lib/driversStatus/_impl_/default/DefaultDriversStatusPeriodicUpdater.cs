using System;
//using System.Diagnostics.Contracts;
using System.Timers;

namespace GCTestTask.Lib
{
    public sealed class DefaultDriversStatusPeriodicUpdater :
                                        IDriversStatusPeriodicUpdater,
                                        IDisposable
    {
        public DefaultDriversStatusPeriodicUpdater(
                                IDriversStatusFetcher fetcher
        ){
            //Contract.Requires(fetcher != null);

            this.fetcher = fetcher;
            this.timer = new Timer {    AutoReset = false,
                                        Enabled = false     };
            this.timer.Elapsed += this.OnTimerPeriodEnd;
        }

        public void StartPeriodicUpdates(uint periodMs) {
            this.timer.Stop();
            this.timer.Interval = periodMs;
            this.timer.Start();
        }

        public void StopPeriodicUpdates() => this.timer.Stop();

        public event EventHandler UpdateDone;

        public void Dispose() => this.timer.Dispose();


        private void OnTimerPeriodEnd(object sender, ElapsedEventArgs e) {
            this.fetcher.Fetch();
            this.UpdateDone?.Invoke(this, null);
            this.timer.Start();
        }

        private readonly IDriversStatusFetcher fetcher;
        private readonly Timer timer;
    }
}