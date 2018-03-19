using Microsoft.VisualStudio.TestTools.UnitTesting;

using WindowsOS.Lib.Drivers;
using WindowsOS.Lib.Drivers.Installed;
using WindowsOS.Lib.Drivers.Installed.DriversProxies.Default;

namespace GCTestTask.Lib.Tests
{
    [TestClass]
    public class StatusTest
    {
        [TestMethod]
        public void CanGetTestDriverStatus() {
            var pendingChanges = new TestDefaultPendingDriverChangesRegister();
            var driversSource = new TestDriversSource();

            try {
                var dp = new TestDriversProxy(pendingChanges, driversSource);

                {
                    var mn = new DriverModuleName("ACPI");
                    Assert.IsTrue(dp.Query.IsActivated(mn));
                    Assert.IsFalse(dp.Query.IsActivationPending(mn));
                    Assert.IsFalse(dp.Query.IsDeactivationPending(mn));
                }
            }
            finally {
                pendingChanges.Dispose();
                driversSource.Dispose();
            }
        }
    }
}