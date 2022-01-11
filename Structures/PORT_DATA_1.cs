using System.Runtime.InteropServices;

namespace ReinstallSys.Structures
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    struct PORT_DATA_1
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string sztPortName;
        public uint dwVersion;
        public uint dwProtocol;
        public uint cbSize;
        public uint dwReserved;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 49)]
        public string sztHostAddress;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string sztSNMPCommunity;
        public uint dwDoubleSpool;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string sztQueue;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string sztIPAddress;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 540)]
        public byte[] Reserved;
        public uint dwPortNumber;
        public uint dwSNMPEnabled;
        public uint dwSNMPDevIndex;
    }
}
