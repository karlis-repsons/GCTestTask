namespace WindowsOS.Lib.Drivers.Installed
{
    /// <summary>
    /// Must stay immutable.
    /// </summary>
    public class DriverPropName
    {
        public DriverPropName(string str) => this.name = str;

        public static implicit operator string(DriverPropName dpn) => dpn.name;


        private readonly string name;
    }
}