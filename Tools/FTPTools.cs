using FluentFTP;
using System;
using System.Threading.Tasks;

namespace ReinstallSys.Tools
{
    internal class FTPTools
    {
        private FtpClient client;
        public FTPTools(string FTPAddress, string FTPUsername, string FTPPassword)
        {
            client = new FtpClient(FTPAddress, FTPUsername, FTPPassword);
        }
        public async Task<FtpStatus> DownFileAsync(string FileSaveUri, string FileRemoveUri)
        {
            await client.ConnectAsync();
            var t = await client.DownloadFileAsync(FileSaveUri, FileRemoveUri, FtpLocalExists.Overwrite, FtpVerify.Retry);
            await client.DisconnectAsync();
            return t;
        }
        public async Task<FtpStatus> DownFileAsync(string FileSaveUri, string FileRemoveUri, Progress<FtpProgress> progress)
        {
            await client.ConnectAsync();
            var t = await client.DownloadFileAsync(FileSaveUri, FileRemoveUri, FtpLocalExists.Overwrite, FtpVerify.Retry, progress);
            await client.DisconnectAsync();
            return t;
        }
    }
}
