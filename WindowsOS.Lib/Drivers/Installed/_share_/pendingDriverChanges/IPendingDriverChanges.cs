using System.Collections.Generic;

namespace WindowsOS.Lib.Drivers.Installed
{
    /// <summary>
    /// Use this to compensate lack of way
    /// to get correct drivers status after it was changed.
    /// Is thread-safe.
    /// 
    /// Risk: pending driver changes can be caused
    ///       by another process and not appear here.
    /// 
    /// Benefit: At least, WindowsOS.Lib may consider its own
    ///          requested driver status changes.
    /// </summary>
    public interface IPendingDriverChanges
    {
        HashSet<DriverModuleName> Deactivation { get; }

        HashSet<DriverModuleName> Activation { get; }
    }
}