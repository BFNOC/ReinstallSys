using SevenZip;
using System;
using System.IO;

namespace ReinstallSys.Tools
{
    internal class SevenZipTools
    {
        static string sevenZipDLL = AppDomain.CurrentDomain.BaseDirectory + "\\7z.dll";
        public static void UnZIPfile(string FileUri, string ExtractUri, EventHandler<FileInfoEventArgs> eventHandler)
        {
            SevenZip.SevenZipBase.SetLibraryPath(sevenZipDLL);
            var extr = new SevenZip.SevenZipExtractor(FileUri);
            extr.BeginExtractArchive(ExtractUri);
            Console.WriteLine("解压");
            extr.FileExtractionFinished += eventHandler;
        }
    }
}
