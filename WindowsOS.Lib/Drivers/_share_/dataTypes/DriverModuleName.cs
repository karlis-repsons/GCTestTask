using System;

namespace WindowsOS.Lib.Drivers
{
    /// <summary>
    /// A wrapper of string, which allows to explicitly state
    /// that the string is a driver module name. Must stay immutable.
    /// </summary>
    public class DriverModuleName :
                            IComparable,
                            IComparable<DriverModuleName>,
                            IEquatable<DriverModuleName>
    {
        public DriverModuleName(string name) {
            if (string.IsNullOrEmpty(name))
                throw new InvalidDriverModuleName("Null or empty not ok.");

            this.name = name;
        }

        public DriverModuleName(DriverModuleName other) {
            this.name = other.name;
        }

        public override string ToString() => this.name;

        public override bool Equals(object other) {
            if (other is DriverModuleName otherMn)
                return this.name.Equals(otherMn.name);
            return false;
        }

        public override int GetHashCode() => this.name.GetHashCode();

        public bool Equals(DriverModuleName other)
            => this.name.Equals(other.name);

        public int CompareTo(object obj) {
            if (obj is DriverModuleName otherMn)
                return this.name.CompareTo(otherMn.name);
            return -1;
        }

        public int CompareTo(DriverModuleName other) 
            => this.name.CompareTo(other.name);


        private readonly string name;
    }
}