using System;

namespace WindowsOS.Lib
{
    [Serializable]
    public class TwoDriversWithSameModuleName : DriverException
    {
        public TwoDriversWithSameModuleName() { }

        public TwoDriversWithSameModuleName(string message) : base(message) { }
    }
}