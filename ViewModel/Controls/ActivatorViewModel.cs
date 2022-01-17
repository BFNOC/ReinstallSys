using FluentFTP;
using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data;
using ReinstallSys.Data.Model;
using ReinstallSys.MyUserControl;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using System;
using System.Windows;

namespace ReinstallSys.ViewModel.Controls
{
    public class ActivatorViewModel : ViewModelBase<ActivatorModel>
    {


        public static ActivatorModel activator;
        public ActivatorViewModel(DataService dataService)
        {
            activator = dataService.GetActivator();
        }
        private static string ActivatorDir;
        private FtpStatus _ftpStatus;
        public FtpStatus FtpStatus
        {
            get => _ftpStatus;
            set
            {
                _ftpStatus = value;
                if (_ftpStatus == FtpStatus.Success)
                {
                    Dialog.Show(new MyTextDialog("下载完成，请稍后等待解压"));
                    ActivatorDir = GlobalVar.GlobalDownloadOfficeExtrFolder(activator.Name);
                    SevenZipTools.UnZIPfile(GlobalVar.GlobalDownloadOfficeFolder + "\\" + activator.FileURI,
                    ActivatorDir, eventHandler);
                }
            }
        }
        public RelayCommand StartActivator => new(AcCMD);
        private async void AcCMD()
        {
            DirTools.CreateDir(GlobalVar.GlobalDownloadOfficeFolder);
            var ftp = DataService.GetFTPModel();
            FTPTools tool = new(ftp.FTPAddress, ftp.OfficeUsername, ftp.OfficePassword);
            FtpStatus = await tool.DownFileAsync(GlobalVar.GlobalDownloadOfficeFolder + "\\" + activator.FileURI,
                activator.FileURI);
        }
        private event EventHandler<EventArgs> eventHandler = async (s, e) =>
        {
            Console.WriteLine("解压完成");
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                Dialog.Show(new MyTextDialog("解压完成，准备激活\r\n界面消失即激活成功"));
            }));
            await CMDTools.SCAsyncInWorkDir(activator.Arguments, ActivatorDir, InstallEvent);

        };
        private static event EventHandler InstallEvent = (s, e) =>
        {
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                Dialog.Show(new MyTextDialog("激活完成\r\n请进行下一步"));
            }));
        };
    }
}
