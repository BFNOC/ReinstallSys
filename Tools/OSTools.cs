using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Tools
{
    internal class OSTools
    {
        public static string GetOperatingSystemVersion()
        {
            if (Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor == 1)
            {
                return "Windows 7";
            }
            else if (Environment.OSVersion.Version.Major == 10)
            {
                return "Windows 10";
            }
            return "Unknown";
        }
    }
}
