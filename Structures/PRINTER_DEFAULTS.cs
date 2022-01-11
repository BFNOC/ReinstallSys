using System;
using System.Runtime.InteropServices;

namespace ReinstallSys.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    struct PRINTER_DEFAULTS
    {
        public IntPtr pDatatype;
        public IntPtr pDevMode;
        public int DesiredAccess;
    }
}
