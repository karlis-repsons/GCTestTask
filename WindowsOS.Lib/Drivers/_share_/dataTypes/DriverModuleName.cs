namespace WindowsOS.Lib.Drivers
{
    /// <summary>
    /// Must stay immutable.
    /// </summary>
    public class DriverModuleName
    {
        public DriverModuleName(string str) => this.name = str;

        public static implicit operator string(DriverModuleName dmn)
                                                    => dmn.name;

        private readonly string name;
    }
}