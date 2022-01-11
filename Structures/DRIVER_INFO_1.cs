using System.Runtime.InteropServices;

namespace ReinstallSys.Structures
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct DRIVER_INFO_1
    {
        public string pName;
    }
}
