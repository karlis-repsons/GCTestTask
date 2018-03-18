using System;

namespace WindowsOS.Lib
{
    [Serializable]
    public class DriverException : WindowsOSLibException
    {
        public DriverException() { }

        public DriverException(string message) : base(message) { }
        
        public DriverException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}