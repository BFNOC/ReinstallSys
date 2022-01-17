using FluentFTP;
using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data;
using ReinstallSys.Data.Model.PrinterModel;
using ReinstallSys.MyUserControl;
using ReinstallSys.Service.Data;
using ReinstallSys.Service.TaskQueue;
using ReinstallSys.Tools;
using SevenZip;
using System;
using System.Management;
using System.Printing;

namespace ReinstallSys.ViewModel.Controls.PrinterViewModel
{
    public class PrinterEasyModelViewModel : ViewModelBase<PrinterModel>
    {
        public PrinterEasyModelViewModel(DataService dataService) => DataList = dataService.GetPrinterList();
        private static string DriverInstallCommand { get; set; }
        private static string DriverInstallCommandWorkDir { get; set; }
        private static string DriverInstallSystem { get; set; }
        private static string PrinterIP { get; set; }
        private static string PrinterName { get; set; }

        private FtpStatus t;
        public FtpStatus T
        {
            get => t;
            set
            {
                t = value;
                if (t == FtpStatus.Success)
                {
                    Dialog.Show(new TextDialog("下载完成，请稍后等待解压安装"));
                    SevenZipTools.UnZIPfile(GlobalVar.GlobalDownloadPrinterFolder + "\\" + SelectedItem.PrinterDriverName,
                    DriverInstallCommandWorkDir, EventHandler);
                    ButtonContent = "解压中，请勿操作";
                }
            }
        }
        /// <summary>
        /// 是否移除所有打印机
        /// </summary>
        private bool _hasRemoveAllPrinter = true;
        public bool HasRemoveAllPrinter
        {
            get => _hasRemoveAllPrinter;
            set => SetProperty(ref _hasRemoveAllPrinter, value);
        }
        /// <summary>
        /// 用于显示打印机具体信息
        /// </summary>
        private string _printerDetails;
        public string PrinterDetails
        {
            get => _printerDetails;
            set => SetProperty(ref _printerDetails, value);
        }
        /// <summary>
        /// 打印机选项
        /// </summary>
        private PrinterModel _selectedItem;
        public PrinterModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                PrinterDetails = "打印机所在地：" + _selectedItem.OfficeAddress + "\r\n"
                    + "打印机型号：" + _selectedItem.PrinterName + "\r\n"
                    + "打印机所用驱动安装包：" + _selectedItem.PrinterDriverName + "\r\n"
                    + "打印机IP地址：" + _selectedItem.PrinterIP + "\r\n"
                    + "更新时间：" + _selectedItem.UpdateTime;
                DriverInstallCommand = _selectedItem.PrinterDriverInstallCMD;
                DriverInstallCommandWorkDir = GlobalVar.GlobalDownloadPrinterExtrFolder(_selectedItem.PrinterDriverName);
                PrinterIP = _selectedItem.PrinterIP;
                PrinterName = _selectedItem.PrinterName;
                if (OSTools.GetOperatingSystemVersion() == "Windows 10")
                {
                    DriverInstallSystem = _selectedItem.PrinterDriverWin10;
                } 
                else
                {
                    DriverInstallSystem = _selectedItem.PrinterDriverWin7;
                }
            }
        }
        private bool _IsClick;
        public bool IsClick
        {
            get => _IsClick;
            set => SetProperty(ref _IsClick, value);
        }
        private int _downloadProgress;
        public int DownloadProgress
        {
            get => _downloadProgress;
            set => SetProperty(ref _downloadProgress, value);
        }
        private string _buttonContent = "开始安装打印机";
        public string ButtonContent
        {
            get => _buttonContent;
            set => SetProperty(ref _buttonContent, value);
            
        }

        public RelayCommand StartInstallPrinter => new(StartInstallPrinterCMD);

        
        private async void StartInstallPrinterCMD()
        {
            DirTools.CreateDir(GlobalVar.GlobalDownloadPrinterFolder);
            if (HasRemoveAllPrinter)
            {
                PrinterTools.DeletePrinterFromList(PrinterTools.GetPrinterIntPtrList());
            }
            var ftp = DataService.GetFTPModel();
            FTPTools tool = new(ftp.FTPAddress, ftp.PrinterUsername, ftp.PrinterPassword);
            Progress<FtpProgress> progress = new(p =>
            {
                Console.WriteLine(p.Progress);
                DownloadProgress = Convert.ToInt32(p.Progress);
                ButtonContent = "下载中";
                if (p.Progress >= 100)
                {
                    ButtonContent = "下载完成，请稍后";
                }
            });
            PrinterTools.AddMonitorPrinterPort(SelectedItem.PrinterIP, SelectedItem.PrinterIP, "Standard TCP/IP Port");
            T = await tool.DownFileAsync(GlobalVar.GlobalDownloadPrinterFolder + "\\" + _selectedItem.PrinterDriverName, 
                _selectedItem.PrinterDriverName, progress);
        }

        private event EventHandler<EventArgs> EventHandler = async (s, e) =>
        {
            Console.WriteLine("解压完成");
            await CMDTools.SCAsyncInWorkDir(DriverInstallCommand, DriverInstallCommandWorkDir, ExitEvent);
            App.Current.Dispatcher.Invoke((Action)(() =>
            {
                Dialog.Show(new TextDialog("解压完成，准备安装驱动\r\n请勿操作"));
            }));
        };

        private static event EventHandler ExitEvent = (s, e) =>
        {
            PrinterTools.InstallPrinterDriverFromPackage(pszDriverName: DriverInstallSystem, dwFlags: 0);
            PrintServer printServer = new(PrintSystemDesiredAccess.AdministrateServer);
            string[] port = new string[] { PrinterIP };
            try
            {
                var printQueue = printServer.InstallPrintQueue(PrinterName, DriverInstallSystem, port, "WinPrint", PrintQueueAttributes.Direct);
                Console.WriteLine(printQueue.Name);
                PrinterTools.SetDefaultPrinter(printQueue.Name);
                PrinterTools.PrintTestPage(null, null);
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Dialog.Show(new TextDialog("安装完成，已打印测试页\r\n请进行下一步"));
                }));
            }
            catch (Exception ex)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Dialog.Show(new TextDialog(ex.ToString()));
                });
            }
        };
        
    }
}
