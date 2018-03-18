using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;

namespace WindowsOS.Lib.Drivers.Installed
{
    public class DefaultPendingDriverChangesRegister :
                                            IPendingDriverChangesRegister,
                                            IDisposable
    {
        public DefaultPendingDriverChangesRegister() {
            this.rwLock = new ReaderWriterLockSlim();
            this.activation = new HashSet<DriverModuleName>();
            this.deactivation = new HashSet<DriverModuleName>();
        }

        public HashSet<DriverModuleName> Deactivation
            => this.GetModuleNamesCopy(this.deactivation);

        public HashSet<DriverModuleName> Activation
            => this.GetModuleNamesCopy(this.activation);

        public void OnDisableRequest(DriverModuleName mn) {
            rwLock.EnterWriteLock();
            try {
                if (this.activation.Contains(mn))
                    this.activation.Remove(mn);
                else
                    this.deactivation.Add(mn);
            }
            finally {
                rwLock.ExitWriteLock();
            }
        }

        public void OnEnableRequest(DriverModuleName mn) {
            rwLock.EnterWriteLock();
            try {
                if (this.deactivation.Contains(mn))
                    this.deactivation.Remove(mn);
                else
                    this.activation.Add(mn);
            }
            finally {
                rwLock.ExitWriteLock();
            }
        }

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool isCalledByDispose) {
            if (this.isDisposed == true)
                return;

            if (isCalledByDispose)
                this.rwLock.Dispose();

            this.isDisposed = true;
        }

        protected readonly ReaderWriterLockSlim rwLock;
        protected readonly HashSet<DriverModuleName> deactivation;
        protected readonly HashSet<DriverModuleName> activation;


        private HashSet<DriverModuleName>
                GetModuleNamesCopy(HashSet<DriverModuleName> sourceSet)
        {
            this.rwLock.EnterReadLock();
            try {
                var copy = new HashSet<DriverModuleName>(sourceSet);
                return copy;
            }
            finally {
                rwLock.ExitReadLock();
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant() {
            Contract.Invariant(
                   deactivation != null && activation != null
            );
        }

        private bool isDisposed = false;
    }
}