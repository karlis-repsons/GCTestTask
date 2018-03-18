using System;

namespace WindowsOS.Lib
{
    [Serializable]
    public class CannotDisableDriver : DriverException
    {
        public CannotDisableDriver() { }

        public CannotDisableDriver(string message) : base(message) { }
    }
}