using System;
using System.Diagnostics.Contracts;
using System.Timers;

namespace GCTestTask.Lib
{
    public class DefaultDriversStatusPeriodicUpdater :
                                        IDriversStatusPeriodicUpdater,
                                        IDisposable
    {
        public DefaultDriversStatusPeriodicUpdater(
                                IDriversStatusFetcher fetcher
        ){
            Contract.Requires(fetcher != null);

            this.fetcher = fetcher;
            this.timer = new Timer {    AutoReset = true,
                                        Enabled = false     };
            this.timer.Elapsed += this.OnTimerPeriodEnd;
        }

        public void StartPeriodicUpdates(uint periodMs) {
            uint minAllowedUpdatesPeriod = 100; // TODO
            Contract.Requires(
                        periodMs >= minAllowedUpdatesPeriod,
                        string.Format(  "Given updates period {0} < {1}.",
                                        periodMs, minAllowedUpdatesPeriod    )
            );

            this.timer.Stop();
            this.timer.Interval = periodMs;
            this.timer.Start();
        }

        public void StopPeriodicUpdates() => this.timer.Stop();

        public event Action UpdateDone;

        public void Dispose() => this.timer.Dispose();


        private void OnTimerPeriodEnd(object sender, ElapsedEventArgs e) {
            this.fetcher.Fetch();
            this.UpdateDone?.Invoke();
        }

        private readonly IDriversStatusFetcher fetcher;
        private readonly Timer timer;
    }
}