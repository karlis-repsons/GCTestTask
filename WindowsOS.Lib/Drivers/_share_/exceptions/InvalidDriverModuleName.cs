using System;

namespace WindowsOS.Lib
{
    [Serializable]
    public class InvalidDriverModuleName: DriverException
    {
        public InvalidDriverModuleName() { }

        public InvalidDriverModuleName(string message) : base(message) { }
    }
}