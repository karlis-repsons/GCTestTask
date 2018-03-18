using System.Diagnostics.Contracts;

namespace WindowsOS.Lib.Drivers.Installed
{
    /// <summary>
    /// Must stay immutable.
    /// </summary>
    public class DriverPropName
    {
        public DriverPropName(string name) {
            Contract.Requires<InvalidDriverPropName>(
                        string.IsNullOrEmpty(name) == false,
                        "Null or empty not ok."
            );

            this.name = name;
        }

        public static implicit operator string(DriverPropName dpn) => dpn.name;


        private readonly string name;
    }
}