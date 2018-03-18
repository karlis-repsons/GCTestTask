using System.Diagnostics.Contracts;

namespace WindowsOS.Lib.Drivers
{
    /// <summary>
    /// Must stay immutable.
    /// </summary>
    public class DriverModuleName
    {
        public DriverModuleName(string name) {
            Contract.Requires<InvalidDriverModuleName>(
                        string.IsNullOrEmpty(name) == false,
                        "Null or empty not ok."
            );

            this.name = name;
        }

        public static implicit operator string(DriverModuleName dmn)
                                                    => dmn.name;

        private readonly string name;
    }
}