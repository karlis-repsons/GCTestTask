using System;

namespace WindowsOS.Lib
{
    [Serializable]
    public class WindowsOSLibException : Exception
    {
        public WindowsOSLibException() { }

        public WindowsOSLibException(string message) : base(message) { }
        
        public WindowsOSLibException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}