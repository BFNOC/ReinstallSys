using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data
{
    public class GlobalVar
    {
        /// <summary>
        /// 程序运行的文件夹
        /// 示例：E:\soft\[yoursoft]
        /// </summary>
        private static readonly string RunFolder = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 程序使用的下载打印机驱动文件夹
        /// 示例：E:\soft\[yoursoft]\PrinterDownload
        /// </summary>
        public static string GlobalDownloadPrinterFolder = RunFolder + "\\PrinterDownload";
        /// <summary>
        /// 程序使用的打印机驱动解压文件夹
        /// 示例：E:\soft\[yoursoft]\PrinterDownload\[DriverName]
        /// </summary>
        /// <param name="DriverName">驱动包名</param>
        /// <returns>示例：E:\soft\[yoursoft]\PrinterDownload\LJM227</returns>
        public static string GlobalDownloadPrinterExtrFolder(string DriverName)
        {
            return GlobalDownloadPrinterFolder + "\\" + Path.GetFileNameWithoutExtension(DriverName);
        }
        /// <summary>
        /// 程序使用的下载Office文件夹
        /// 示例：E:\soft\[yoursoft]\OfficeDownload
        /// </summary>
        public static string GlobalDownloadOfficeFolder = RunFolder + "\\OfficeDownload";
        /// <summary>
        /// 程序使用的Office驱动解压文件夹
        /// 示例：E:\soft\[yoursoft]\OfficeDownload\[OfficeName]
        /// </summary>
        /// <param name="DriverName">驱动包名</param>
        /// <returns>示例：E:\soft\[yoursoft]\OfficeDownload\LJM227</returns>
        public static string GlobalDownloadOfficeExtrFolder(string OfficeName)
        {
            return GlobalDownloadOfficeFolder + "\\" + Path.GetFileNameWithoutExtension(OfficeName);
        }
        /// <summary>
        /// 程序使用的下载Software文件夹
        /// 示例：E:\soft\[yoursoft]\SoftwareDownload
        /// </summary>
        public static string GlobalDownloadSoftwareFolder = RunFolder + "\\SoftwareDownload";
    }
}
