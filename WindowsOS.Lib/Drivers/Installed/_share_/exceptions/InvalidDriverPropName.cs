using System;

namespace WindowsOS.Lib
{
    [Serializable]
    public class InvalidDriverPropName : DriverException
    {
        public InvalidDriverPropName() { }

        public InvalidDriverPropName(string message) : base(message) { }

        public InvalidDriverPropName(string message, Exception innerException)
            : base(message, innerException) { }
    }
}