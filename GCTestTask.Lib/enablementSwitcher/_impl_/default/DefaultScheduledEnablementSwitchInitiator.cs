using System;
using System.Collections.Generic;
using System.Timers;

namespace GCTestTask.Lib
{
    public sealed class DefaultScheduledEnablementSwitchInitiator :
                                            IScheduledEnablementSwitchInitiator,
                                            IDisposable
    {
        public DefaultScheduledEnablementSwitchInitiator(
                                    DriverEnablementSchedule schedule,
                                    IOneDriverEnablementSwitch enablementSwitch
        ){
            this.schedule = schedule;
            this.enablementSwitch = enablementSwitch;

            this.timepointToTimer
                    = new Dictionary<DateTime, Timer>(schedule.Count);

            foreach (DateTime time in schedule.Keys) {
                var timer = new Timer {   AutoReset = false,
                                          Enabled = false      };
                timer.Elapsed
                    += (sender, ea) =>
                            this.HandleDriverEnablementRequest(
                                                sender, ea, schedule[time]   );

                this.timepointToTimer[time] = timer;
            }
        }

        public void Start() {
            foreach (DateTime scheduledTimepoint in schedule.Keys) {
                DateTime now = DateTime.Now;
                double msTillScheduledTimepoint
                            = (scheduledTimepoint - now).TotalMilliseconds;

                if (msTillScheduledTimepoint > 0) {
                    Timer timer = this.timepointToTimer[scheduledTimepoint];
                    timer.Interval = msTillScheduledTimepoint;
                    timer.Start();
                }
            }
        }

        public void Dispose() {
            foreach (Timer timer in this.timepointToTimer.Values)
                timer.Dispose();
        }


        private void HandleDriverEnablementRequest(
                                                object sender, 
                                                ElapsedEventArgs ea, 
                                                DriverEnablementRequest request
        ){
            if (request == DriverEnablementRequest.Activation)
                this.enablementSwitch.RequestActivation();

            if (request == DriverEnablementRequest.Deactivation)
                this.enablementSwitch.RequestDeactivation();
        }

        private readonly DriverEnablementSchedule schedule;
        private readonly IOneDriverEnablementSwitch enablementSwitch;
        private readonly Dictionary<DateTime, Timer> timepointToTimer;
    }
}