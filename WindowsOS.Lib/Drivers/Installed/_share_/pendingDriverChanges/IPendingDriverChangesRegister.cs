namespace WindowsOS.Lib.Drivers.Installed
{
    /// <summary>
    /// Use this to compensate lack of way
    /// to get correct drivers status after it was changed.
    /// 
    /// Risk: pending driver changes can be caused
    ///       by another process and not appear here.
    /// </summary>
    public interface IPendingDriverChangesRegister : IPendingDriverChanges
    {
        void OnDisableRequest(DriverModuleName moduleName);

        void OnEnableRequest(DriverModuleName moduleName);
    }
}