//using System.Diagnostics.Contracts;

using System;

namespace WindowsOS.Lib.Drivers.Installed
{
    /// <summary>
    /// A wrapper of string, which allows to explicitly state
    /// that the string is a driver prop name. Must stay immutable.
    /// </summary>
    public class DriverPropName :
                            IComparable,
                            IComparable<DriverPropName>,
                            IEquatable<DriverPropName>
    {
        public DriverPropName(string name) {
            //Contract.Requires<InvalidDriverPropName>(
            //            string.IsNullOrEmpty(name) == false,
            //            "Null or empty not ok."
            //);
            if (string.IsNullOrEmpty(name))
                throw new InvalidDriverPropName("Null or empty not ok.");

            this.name = name;
        }

        public DriverPropName(DriverPropName other) {
            this.name = other.name;
        }

        public override string ToString() => this.name;

        public override bool Equals(object other) {
            if (other is DriverPropName otherPn)
                return this.name.Equals(otherPn.name);
            return false;
        }

        public override int GetHashCode() => this.name.GetHashCode();

        public bool Equals(DriverPropName other)
            => this.name.Equals(other.name);

        public int CompareTo(object other) {
            if (other is DriverPropName otherPn)
                return this.name.CompareTo(otherPn.name);
            return -1;
        }

        public int CompareTo(DriverPropName other)
            => this.name.CompareTo(other.name);


        private readonly string name;
    }
}