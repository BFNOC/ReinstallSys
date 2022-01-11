using ReinstallSys.Properties;
using ReinstallSys.Structures;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReinstallSys.Tools
{
    internal class PrinterTools
    {
        [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeletePrinterConnection(string pName);
        [DllImport("winspool.drv", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        static extern bool DeletePrinter(IntPtr hPrinter);

        enum PrinterEnumFlags
        {
            PRINTER_ENUM_DEFAULT = 0x00000001,
            PRINTER_ENUM_LOCAL = 0x00000002,
            PRINTER_ENUM_CONNECTIONS = 0x00000004,
            PRINTER_ENUM_FAVORITE = 0x00000004,
            PRINTER_ENUM_NAME = 0x00000008,
            PRINTER_ENUM_REMOTE = 0x00000010,
            PRINTER_ENUM_SHARED = 0x00000020,
            PRINTER_ENUM_NETWORK = 0x00000040,
            PRINTER_ENUM_EXPAND = 0x00004000,
            PRINTER_ENUM_CONTAINER = 0x00008000,
            PRINTER_ENUM_ICONMASK = 0x00ff0000,
            PRINTER_ENUM_ICON1 = 0x00010000,
            PRINTER_ENUM_ICON2 = 0x00020000,
            PRINTER_ENUM_ICON3 = 0x00040000,
            PRINTER_ENUM_ICON4 = 0x00080000,
            PRINTER_ENUM_ICON5 = 0x00100000,
            PRINTER_ENUM_ICON6 = 0x00200000,
            PRINTER_ENUM_ICON7 = 0x00400000,
            PRINTER_ENUM_ICON8 = 0x00800000,
            PRINTER_ENUM_HIDE = 0x01000000,
            PRINTER_ENUM_CATEGORY_ALL = 0x02000000,
            PRINTER_ENUM_CATEGORY_3D = 0x04000000
        }
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumPrinters(PrinterEnumFlags Flags, string Name, uint Level, IntPtr pPrinterEnum, uint cbBuf, ref uint pcbNeeded, ref uint pcReturned);
        [DllImport("winspool.drv", SetLastError = true)]
        static extern int OpenPrinter(string pPrinterName, out IntPtr phPrinter, ref PRINTER_DEFAULTS pDefault);
        [DllImport("winspool.drv", EntryPoint = "ClosePrinter")]
        private static extern int ClosePrinter(IntPtr hPrinter);
        /**
         * Printer_ACCESS
         */
        private const int SERVER_ADMIN = 0x01;
        private const int SERVER_ENUM = 0x02;
        private const int PRINTER_ACCESS_ADMINISTRATOR = 0x4;
        private const int PRINTER_ACCESS_USE = 0x8;
        private const int JOB_ADMIN = 0x10;
        private const int JOB_READ = 0x20;
        private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private const int PRINTER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTRATOR | PRINTER_ACCESS_USE;
        private const int ERROR_INSUFFICIENT_BUFFER = 122;

        //----
        [DllImport("winspool.drv", EntryPoint = "XcvDataW")]
        private static extern bool XcvData(
            IntPtr hXcv,
            [MarshalAs(UnmanagedType.LPWStr)] string pszDataName,
            IntPtr pInputData,
            uint cbInputData,
            IntPtr pOutputData,
            uint cbOutputData,
            out uint pcbOutputNeeded,
            out uint pwdStatus);


        public static List<IntPtr> GetPrinterIntPtrList()
        {
            List<IntPtr> PrinterList = new();
            var printers = PrinterSettings.InstalledPrinters;
            foreach (var printer in printers)
            {
                PRINTER_DEFAULTS pd = new()
                {
                    pDatatype = IntPtr.Zero,
                    pDevMode = IntPtr.Zero,
                    DesiredAccess = PRINTER_ALL_ACCESS
                };
                OpenPrinter(printer.ToString(), out IntPtr pint, ref pd);
                PrinterList.Add(pint);
            }
            return PrinterList;
        }

        public static bool DeletePrinterFromList(List<IntPtr> printerList)
        {
            foreach (var i in printerList)
            {
                if (!DeletePrinter(i)) return false;
            }
            return true;
        }

        /// <summary>
        /// add a standard TCP/IP Port used by the printer.
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="IPAddress"></param>
        /// <param name="portType">example:Standard TCP/IP Port</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static bool AddMonitorPrinterPort(string portName, string IPAddress, string portType)
        {
            IntPtr printerHandle;
            PRINTER_DEFAULTS defaults = new() { DesiredAccess = SERVER_ADMIN };
            if (OpenPrinter(",XcvMonitor " + portType, out printerHandle, ref defaults) != 1)
                throw new Exception("Could not open printer for the monitor port " + portType + "!");
            try
            {
                PORT_DATA_1 portData = new()
                {
                    dwVersion = 1,
                    dwProtocol = 1, // 1 = RAW, 2 = LPR
                    dwPortNumber = 9100, // 9100 = default port for RAW, 515 for LPR
                    dwReserved = 0,
                    sztPortName = portName,
                    sztIPAddress = IPAddress,
                    sztHostAddress = IPAddress,
                    sztSNMPCommunity = "public",
                    dwSNMPEnabled = 1,
                    dwSNMPDevIndex = 1
                };
                uint size = (uint)Marshal.SizeOf(portData);
                portData.cbSize = size;
                IntPtr pointer = Marshal.AllocHGlobal((int)size);
                Marshal.StructureToPtr(portData, pointer, true);
                uint outputNeeded;
                try
                {
                    if (!XcvData(printerHandle, "AddPort", pointer, size, IntPtr.Zero, 0, out outputNeeded, out uint status))
                        throw new Exception("Could not add port (error code " + status + ")!");
                }
                finally
                {
                    Marshal.FreeHGlobal(pointer);
                }
            }
            finally
            {
                ClosePrinter(printerHandle);
            }
            return true;
        }
        public static int AddLocalPort(string portName)
        {
            PRINTER_DEFAULTS def = new PRINTER_DEFAULTS();

            def.pDatatype = IntPtr.Zero;
            def.pDevMode = IntPtr.Zero;
            def.DesiredAccess = 1; //Server Access Administer

            IntPtr hPrinter = IntPtr.Zero;

            int n = OpenPrinter(",XcvMonitor Local Port", out hPrinter,ref def);
            if (n == 0)
                return Marshal.GetLastWin32Error();

            if (!portName.EndsWith("\0"))
                portName += "\0"; // Must be a null terminated string

            // Must get the size in bytes. Rememeber .NET strings are formed by 2-byte characters
            uint size = (uint)(portName.Length * 2);

            // Alloc memory in HGlobal to set the portName
            IntPtr portPtr = Marshal.AllocHGlobal((int)size);
            Marshal.Copy(portName.ToCharArray(), 0, portPtr, portName.Length);

            uint needed; // Not that needed in fact...
            uint xcvResult; // Will receive de result here

            XcvData(hPrinter, "AddPort", portPtr, size, IntPtr.Zero, 0, out needed, out xcvResult);

            ClosePrinter(hPrinter);
            Marshal.FreeHGlobal(portPtr);

            return (int)xcvResult;
        }
    }
}
