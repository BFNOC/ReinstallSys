using ReinstallSys.Structures;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Tools
{
    internal class PrinterTools
    {
        [DllImport("winspool.drv", CharSet = CharSet.Ansi, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeletePrinterConnection(String pName);
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
        /**
         * Printer_ACCESS
         */
        private const int PRINTER_ACCESS_ADMINISTRATOR = 0x4;
        private const int PRINTER_ACCESS_USE = 0x8;
        private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private const int PRINTER_ALL_ACCESS = STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTRATOR | PRINTER_ACCESS_USE;
        private const int ERROR_INSUFFICIENT_BUFFER = 122;
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
    }
}
