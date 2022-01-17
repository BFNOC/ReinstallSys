using FluentFTP;
using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data;
using ReinstallSys.Data.Model;
using ReinstallSys.MyUserControl;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReinstallSys.ViewModel.Controls
{
    public class OfficeInstallViewModel : ViewModelBase<OfficeInstallModel>, INotifyPropertyChanged
    {
        protected string OperatingSystem;
        private static string InstallCommand;
        private static string InstallArguments;
        private static string InstallWorkDir;
        public OfficeInstallViewModel(DataService dataService)
        {
            DataList = dataService.GetOfficeInstallList();
            OfficeUninstallList = dataService.GetOfficeUninstallList();
            OperatingSystem = OSTools.GetOperatingSystemVersion();
            if (OperatingSystem == "Windows 7")
            {
                DataList.Remove(DataList.Single(item => item.Name == "Office 2019"));
                DataList.Remove(DataList.Single(item => item.Name == "Office 2021"));
            }
        }

        private List<OfficeUninstallModel> _officeUninstallList;
        public List<OfficeUninstallModel> OfficeUninstallList
        {
            get => _officeUninstallList;
            set => SetProperty(ref _officeUninstallList, value);
        }
        private OfficeInstallModel _selectedOfficeInstall;
        public OfficeInstallModel SelectedOfficeInstall
        {
            get => _selectedOfficeInstall;
            set
            {
                SetProperty(ref _selectedOfficeInstall, value);
                InstallCommand = _selectedOfficeInstall.InstallCommand;
                InstallArguments = _selectedOfficeInstall.InstallArguments;
                InstallWorkDir = GlobalVar.GlobalDownloadOfficeExtrFolder(_selectedOfficeInstall.Name);
            }
        }
        private OfficeUninstallModel _selectedOfficeUninstall;
        public OfficeUninstallModel SelectedOfficeUninstall
        {
            get => _selectedOfficeUninstall;
            set => SetProperty(ref _selectedOfficeUninstall, value);
        }
        private int _downloadProgress;
        public int DownloadProgress
        {
            get => _downloadProgress;
            set => SetProperty(ref _downloadProgress, value);
        }
        private string _startProcessBtnContent = "开始执行";
        public string StartProcessBtnContent
        {
            get => _startProcessBtnContent;
            set => SetProperty(ref _startProcessBtnContent, value);
        }
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
                    SevenZipTools.UnZIPfile(GlobalVar.GlobalDownloadOfficeFolder + "\\" + SelectedOfficeInstall.FileURI,
                    InstallWorkDir, StartProcessEventHandler);
                    StartProcessBtnContent = "解压中，请勿操作";
                }
            }
        }
        private FtpStatus _uninstallFtpStatus;
        public FtpStatus UninstallFtpStatus
        {
            get => _uninstallFtpStatus;
            set
            {
                _uninstallFtpStatus = value;
                if (_uninstallFtpStatus == FtpStatus.Success)
                {
                    Dialog.Show(new MyTextDialog("下载完成，请稍后等待卸载执行"));
                    CMDTools.SC(SelectedOfficeUninstall.Uninstall_command, GlobalVar.GlobalDownloadOfficeFolder);
                }
            }
        }

        private async Task<bool> StartUninstallOffice()
        {
            DirTools.CreateDir(GlobalVar.GlobalDownloadOfficeFolder);
            var ftp = DataService.GetFTPModel();
            FTPTools tool = new(ftp.FTPAddress, ftp.OfficeUsername, ftp.OfficePassword);
            Progress<FtpProgress> progress = new(p =>
            {
                Console.WriteLine(p.Progress);
            });
            await tool.DownFileAsync(GlobalVar.GlobalDownloadOfficeFolder + "\\" + SelectedOfficeUninstall.FileURL,
                SelectedOfficeUninstall.FileURL, progress);
            return true;
        }

        public RelayCommand StartProcess => new(StartProcessCMD);
        private async void StartProcessCMD()
        {
            if (SelectedOfficeUninstall != null)
            {
                await StartUninstallOffice();
            }
            DirTools.CreateDir(GlobalVar.GlobalDownloadOfficeFolder);
            var ftp = DataService.GetFTPModel();
            FTPTools tool = new(ftp.FTPAddress, ftp.OfficeUsername, ftp.OfficePassword);
            Progress<FtpProgress> progress = new(p =>
            {
                Console.WriteLine(p.Progress);
                DownloadProgress = Convert.ToInt32(p.Progress);
                StartProcessBtnContent = "下载中";
                if (p.Progress >= 100)
                {
                    StartProcessBtnContent = "下载完成，请稍后";
                }
            });
            FtpStatus = await tool.DownFileAsync(GlobalVar.GlobalDownloadOfficeFolder + "\\" + SelectedOfficeInstall.FileURI,
                SelectedOfficeInstall.FileURI, progress);
        }
        private event EventHandler<EventArgs> StartProcessEventHandler = async (s, e) =>
        {
            Console.WriteLine("解压完成");
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                Dialog.Show(new MyTextDialog("解压完成，准备安装\r\n请勿操作"));
            }));
            await CMDTools.SCAsyncInWorkDir(InstallCommand + " " + InstallArguments, InstallWorkDir, InstallEvent);
            
        };
        private static event EventHandler InstallEvent = (s, e) =>
        {
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                Dialog.Show(new MyTextDialog("安装完成\r\n请进行下一步"));
            }));
        };
    }
}
