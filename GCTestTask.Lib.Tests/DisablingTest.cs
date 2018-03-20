using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;
using WindowsOS.Lib.Drivers.Installed.DriversProxies.Default;

namespace GCTestTask.Lib.Tests
{
    [TestClass]
    public class DisablingTest
    {
        [TestMethod]
        public void CanDisableAtGivenTime() {
            int deactivateAfterMs = 200,
                checkAfterMs = 200;

            this.ExecuteTest((proxy, pendingChanges, driversSource) => 
            {
                var moduleName = new DriverModuleName("ACPI");

                var switcher = new DriversScheduledEnablementSwitcher(
                                        proxy.Query,
                                        proxy.Manage,
                                        pendingChanges
                );

                var schedule
                        = new DriverEnablementSchedule {
                                { 
                                    DateTime.Now.AddMilliseconds(
                                                        deactivateAfterMs),
                                    DriverEnablementRequest.Deactivation
                                }
                };
                
                switcher.Disable(moduleName, schedule);
                Assert.IsTrue(driversSource.GetStatus(moduleName)
                                           .IsDeactivationPending == false);

                Thread.Sleep(deactivateAfterMs + checkAfterMs);

                Assert.IsTrue(driversSource.GetStatus(moduleName)
                                           .IsDeactivationPending == true   );
            });
        }


        private void ExecuteTest(
                                    Action< ITestDriversProxy,
                                            IPendingDriverChangesRegister,
                                            ITestDriversSource               >
                                    test
        ){
            var pendingChanges = new TestDefaultPendingDriverChangesRegister();
            var driversSource = new TestDriversSource();
            var proxy = new TestDriversProxy(pendingChanges, driversSource);

            try {
                test(proxy, pendingChanges, driversSource);
            }
            finally {
                pendingChanges.Dispose();
                driversSource.Dispose();
            }
        }
    }
}