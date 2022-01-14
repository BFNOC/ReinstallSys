using SevenZip;
using System;
using System.IO;

namespace ReinstallSys.Tools
{
    internal class SevenZipTools
    {
        static string sevenZipDLL = AppDomain.CurrentDomain.BaseDirectory + "\\7z.dll";
        public static void UnZIPfile(string FileUri, string ExtractUri, EventHandler<EventArgs> eventHandler)
        {
            SevenZip.SevenZipBase.SetLibraryPath(sevenZipDLL);
            var extr = new SevenZip.SevenZipExtractor(FileUri);
            extr.BeginExtractArchive(ExtractUri);
            Console.WriteLine("解压开始");
            extr.ExtractionFinished += eventHandler;
        }
    }
}
