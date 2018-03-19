using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GCTestTask.Lib
{
    [Serializable]
    public class DriverEnablementSchedule :
                    Dictionary<DateTime, DriverEnablementRequest>
    {
        // forward construction to Dictionary base class:
        // ----------------------------------------------
        public DriverEnablementSchedule() { }

        public DriverEnablementSchedule(int capacity) : base(capacity) { }

        public DriverEnablementSchedule(IEqualityComparer<DateTime> comparer)
                    : base(comparer) { }

        public DriverEnablementSchedule(
                    IDictionary<DateTime, DriverEnablementRequest> dictionary
        ) : base(dictionary) { }

        public DriverEnablementSchedule(
                    int capacity,
                    IEqualityComparer<DateTime> comparer
        ) : base(capacity, comparer) { }
        
        public DriverEnablementSchedule(
                    IDictionary<DateTime, DriverEnablementRequest> dictionary,
                    IEqualityComparer<DateTime> comparer
        ) : base(dictionary, comparer) { }

        protected DriverEnablementSchedule(
                    SerializationInfo info,
                    StreamingContext context
        ) : base (info, context) { }
        // ==============================================
    }
}