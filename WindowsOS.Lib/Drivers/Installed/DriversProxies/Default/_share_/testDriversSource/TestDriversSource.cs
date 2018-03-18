using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;

namespace WindowsOS.Lib.Drivers.Installed.DriversProxies.Default
{
    using DPNs = TestDriverPropNames;

    public class TestDriversSource : ITestDriversSource, IDisposable
    {
        public TestDriversSource() {
            this.propsLock = new ReaderWriterLockSlim();
            this.statusesLock = new ReaderWriterLockSlim();

            this.SetDefaults();
        }

        public List<Dictionary<DriverPropName, string>>
               GetPropDictionariesCopy()
        {
            Contract.Ensures(Contract.Result<
                        List<Dictionary<DriverPropName, string>>   >() != null);
            // TODO: consider Contract.ForAll to Ensure elements (might not work)

            propsLock.EnterReadLock();

            try {
                var copy = new List<Dictionary<DriverPropName, string>>();

                foreach (Dictionary<DriverPropName, string>
                         source1DriverProps in this.allDriversPropDicts
                ) {
                    var dest1DriverProps
                            = new Dictionary<DriverPropName, string>();

                    foreach (DriverPropName propName in source1DriverProps.Keys)
                            dest1DriverProps[propName] =
                          source1DriverProps[propName];

                    copy.Add(dest1DriverProps);
                }

                return copy;
            }
            finally {
                propsLock.ExitReadLock();
            }
        }

        public Dictionary<DriverModuleName, DriverStatus> 
               GetStatusDictionaryCopy()
        {
            Contract.Ensures(Contract.Result<
                                Dictionary<DriverModuleName, DriverStatus>
                        >() != null
            );

            statusesLock.EnterReadLock();

            try {
                var copy = new Dictionary<DriverModuleName, DriverStatus>();

                foreach (DriverModuleName dmn in this.driversStatus.Keys) {
                    DriverStatus source = this.driversStatus[dmn];
                    var destStatus = new DriverStatus {
                            IsActivated = source.IsActivated,
                            IsDeactivationPending = source.IsDeactivationPending,
                            IsActivationPending = source.IsActivationPending
                    };

                    copy[dmn] = destStatus;
                }

                return copy;
            }
            finally {
                statusesLock.ExitReadLock();
            }
        }

        public void SetPropDictionaries(
                        List<Dictionary<DriverPropName, string>> pd
        ){
            this.propsLock.EnterWriteLock();
            this.allDriversPropDicts = pd;
            this.propsLock.ExitWriteLock();
        }

        public void SetStatusDictionary(
                        Dictionary<DriverModuleName, DriverStatus> ds
        ){
            this.statusesLock.EnterWriteLock();
            this.driversStatus = ds;
            this.statusesLock.ExitWriteLock();
        }

        public string GetProp(DriverModuleName mn, DriverPropName pn) {
            this.propsLock.EnterReadLock();
            try {
                foreach (Dictionary<DriverPropName, string>
                         driverPropDict in this.allDriversPropDicts
                )
                    if (driverPropDict[DPNs.ModuleName] == mn) {
                        if (driverPropDict.ContainsKey(pn) == false)
                            throw new InvalidDriverPropName(pn);

                        return driverPropDict[pn];
                    }

                throw new InvalidDriverModuleName(mn);
            }
            finally {
                this.propsLock.ExitReadLock();
            }
        }

        public void SetProp(
                                DriverModuleName mn, 
                                DriverPropName pn, 
                                string value
        ){
            this.propsLock.EnterWriteLock();
            try {
                foreach (Dictionary<DriverPropName, string>
                         driverPropDict in this.allDriversPropDicts
                )
                    if (driverPropDict[DPNs.ModuleName] == mn) {
                        if (driverPropDict.ContainsKey(pn) == false)
                            throw new InvalidDriverPropName(pn);

                        driverPropDict[pn] = value;
                    }

                throw new InvalidDriverModuleName(mn);
            }
            finally {
                this.propsLock.ExitWriteLock();
            }
        }

        public DriverStatus GetStatus(DriverModuleName n) {
            this.statusesLock.EnterReadLock();
            try {
                if (this.driversStatus.ContainsKey(n) == false)
                    throw new InvalidDriverModuleName(n);

                return this.driversStatus[n];
            }
            finally {
                this.statusesLock.ExitReadLock();
            }
        }

        public void SetStatus(DriverModuleName n, DriverStatus s) {
            this.statusesLock.EnterWriteLock();
            try {
                if (this.driversStatus.ContainsKey(n) == false)
                    throw new InvalidDriverModuleName(n);

                this.driversStatus[n] = s;
            }
            finally {
                this.statusesLock.ExitWriteLock();
            }
        }

        public void SetDefaults() {
            this.propsLock.EnterWriteLock();
            
            try {
                this.allDriversPropDicts
                    = new List<Dictionary<DriverPropName, string>> {
                        new Dictionary<DriverPropName, string>
                        {
                            { DPNs.ModuleName, "1394ohci" },
                            { DPNs.DisplayName,
                                "1394 OHCI Compliant Host Controller" },
                            { DPNs.SupportsDisabling, "False" },
                            { DPNs.RequiresRestartToWork, "False" }
                        },
                        new Dictionary<DriverPropName, string>
                        {
                            { DPNs.ModuleName, "ACPI" },
                            { DPNs.DisplayName, "Microsoft ACPI Driver" },
                            { DPNs.SupportsDisabling, "True" },
                            { DPNs.RequiresRestartToWork, "False" }
                        },
                        new Dictionary<DriverPropName, string>
                        {
                            { DPNs.ModuleName, "exfat" },
                            { DPNs.DisplayName, "exFAT File System Driver" },
                            { DPNs.SupportsDisabling, "False" },
                            { DPNs.RequiresRestartToWork, "False" }
                        },
                        new Dictionary<DriverPropName, string>
                        {
                            { DPNs.ModuleName, "IntcDAud" },
                            { DPNs.DisplayName, "Intel(R) Display Audio" },
                            { DPNs.SupportsDisabling, "True" },
                            { DPNs.RequiresRestartToWork, "False" }
                        },
                        new Dictionary<DriverPropName, string>
                        {
                            { DPNs.ModuleName, "TPM" },
                            { DPNs.DisplayName, "TPM" },
                            { DPNs.SupportsDisabling, "True" },
                            { DPNs.RequiresRestartToWork, "True" } // maybe not
                        }
                    }
                ;
            }
            finally {
                this.propsLock.ExitWriteLock();
            }

            this.statusesLock.EnterWriteLock();
            try {
                this.driversStatus
                    = new Dictionary<DriverModuleName, DriverStatus>
                    {
                        {
                            new DriverModuleName("1394ohci")
                          , new DriverStatus {
                                IsActivated = true,
                                IsDeactivationPending = false,
                                IsActivationPending = false }
                        },
                        {
                            new DriverModuleName("ACPI")
                          , new DriverStatus {
                                IsActivated = true,
                                IsDeactivationPending = false,
                                IsActivationPending = false }
                        },
                        {
                            new DriverModuleName("exfat")
                          , new DriverStatus {
                                IsActivated = false,
                                IsDeactivationPending = false,
                                IsActivationPending = false }
                        },
                        {
                            new DriverModuleName("IntcDAud")
                          , new DriverStatus {
                                IsActivated = true,
                                IsDeactivationPending = false,
                                IsActivationPending = false }
                        },
                        {
                            new DriverModuleName("TPM")
                          , new DriverStatus {
                                IsActivated = true,
                                IsDeactivationPending = false,
                                IsActivationPending = false }
                        },

                    }
                ;
            }
            finally {
                this.statusesLock.ExitWriteLock();
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool isCalledByDispose) {
            if (this.isDisposed == true)
                return;

            if (isCalledByDispose) {
                this.propsLock.Dispose();
                this.statusesLock.Dispose();
            }

            this.allDriversPropDicts = null;
            this.driversStatus = null;
            this.isDisposed = true;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant() {
            Contract.Invariant(
                   allDriversPropDicts != null
                && driversStatus != null
            );
        }

        private readonly ReaderWriterLockSlim propsLock;
        private readonly ReaderWriterLockSlim statusesLock;
        private List<Dictionary<DriverPropName, string>> allDriversPropDicts;
        private Dictionary<DriverModuleName, DriverStatus> driversStatus;
        private bool isDisposed = false;
    }
}