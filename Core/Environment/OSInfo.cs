using Core.Enums;

namespace Core.Environment
{
    public class OSInfo
    {
        /// <summary>
        /// Return the Platform of the operating system. e.g. Windows, OS X, Linux
        /// </summary>
        public  OSSystem          Platform { get; }

        public OSInfo(IOSSystemResolver osSystemResolver = default)
        {
            osSystemResolver = osSystemResolver ?? new OSSystemResolver();
            Platform           = osSystemResolver.Detect();
        }
    }
}
