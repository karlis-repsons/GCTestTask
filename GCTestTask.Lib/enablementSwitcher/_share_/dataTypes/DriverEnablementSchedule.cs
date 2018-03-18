using System;
using System.Collections.Generic;

namespace GCTestTask.Lib
{
    public class DriverEnablementSchedule :
                    Dictionary<DateTime, DriverEnablementRequest>
    {
        // forward construction to Dictionary base class:
        // ----------------------------------------------
        public DriverEnablementSchedule() { }
        
        public DriverEnablementSchedule(
                    IDictionary<DateTime, DriverEnablementRequest> dictionary
        ) : base(dictionary) { }

        public DriverEnablementSchedule(int capacity) : base(capacity) { }

        public DriverEnablementSchedule(
                    IEnumerable<KeyValuePair<DateTime, DriverEnablementRequest>>
                    collection
        ) : base(collection) { }

        public DriverEnablementSchedule(IEqualityComparer<DateTime> comparer)
            : base(comparer) { }

        public DriverEnablementSchedule(
                    int capacity,
                    IEqualityComparer<DateTime> comparer
        ) : base(capacity, comparer) { }
        
        public DriverEnablementSchedule(
                    IDictionary<DateTime, DriverEnablementRequest> dictionary,
                    IEqualityComparer<DateTime> comparer
        ) : base(dictionary, comparer) { }

        public DriverEnablementSchedule(
                    IEnumerable<KeyValuePair<DateTime, DriverEnablementRequest>>
                    collection
                  , IEqualityComparer<DateTime> comparer
        ) : base (collection, comparer) { }
        // ==============================================
    }
}