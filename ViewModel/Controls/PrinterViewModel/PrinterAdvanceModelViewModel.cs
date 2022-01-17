using FluentFTP;
using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data;
using ReinstallSys.Data.Model.PrinterModel;
using ReinstallSys.MyUserControl;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.Printing;
using System.Windows;

namespace ReinstallSys.ViewModel.Controls.PrinterViewModel
{
    public class PrinterAdvanceModelViewModel : ViewModelBase<PrinterModel>
    {
        public PrinterAdvanceModelViewModel(DataService dataService) 
        {
            DataList = dataService.GetPrinterList();
            PrinterIP = dataService.GetPrinterIPList();
        }
        private static string DriverInstallCommand { get; set; }
        private static string DriverInstallCommandWorkDir { get; set; }
        private static string DriverInstallSystem { get; set; }
        private static string EventUsePrinterIP { get; set; }
        private static string EventUsePrinterName { get; set; }

        private string _oDLDriverBtnConntent = "仅下载驱动包";
        public string ODLDriverBtnConntent
        {
            get => _oDLDriverBtnConntent;
            set => SetProperty(ref _oDLDriverBtnConntent, value);
        }
        private string _startInstallBtnConntent = "开始安装打印机";
        public string StartInstallBtnConntent
        {
            get => _startInstallBtnConntent;
            set => SetProperty(ref _startInstallBtnConntent, value);
        }
        private int _driverDownloadProgress;
        public int DriverDownloadProgress
        {
            get => _driverDownloadProgress;
            set => SetProperty(ref _driverDownloadProgress, value);
        }
        private int _startInstallProgress;
        public int StartInstallProgress
        {
            get => _startInstallProgress;
            set => SetProperty(ref _startInstallProgress, value);
        }
        private FtpStatus _oDLDriverFTPStatus;
        public FtpStatus ODLDriverFTPStatus
        {
            get => _oDLDriverFTPStatus;
            set
            {
                _oDLDriverFTPStatus = value;
                if (_oDLDriverFTPStatus == FtpStatus.Success)
                {
                    Dialog.Show(new TextDialog("下载完成，请稍后等待解压"));
                    SevenZipTools.UnZIPfile(GlobalVar.GlobalDownloadPrinterFolder + "\\" + SelectedItem.PrinterDriverName,
                    DriverInstallCommandWorkDir, ODLEventHandler);
                    ODLDriverBtnConntent = "解压中，请勿操作";
                }
            }
        }
        private FtpStatus _startInstallFTPStatus;
        public FtpStatus StartInstallFTPStatus
        {
            get => _startInstallFTPStatus;
            set
            {
                _startInstallFTPStatus = value;
                if (_startInstallFTPStatus == FtpStatus.Success)
                {
                    Dialog.Show(new TextDialog("下载完成，请稍后等待解压"));
                    SevenZipTools.UnZIPfile(GlobalVar.GlobalDownloadPrinterFolder + "\\" + SelectedItem.PrinterDriverName,
                    DriverInstallCommandWorkDir, StartInstallEventHandler);
                    StartInstallBtnConntent = "解压中，请勿操作";
                }
            }
        }
        /// <summary>
        /// IP列表
        /// </summary>
        private List<string> _printerIP;
        public List<string> PrinterIP
        {
            get => _printerIP;
            set => SetProperty(ref _printerIP, value);
        }
        /// <summary>
        /// 选择的IP地址
        /// </summary>
        private string _printerIPselectedItem;
        public string PrinterIPselectedItem
        {
            get => _printerIPselectedItem;
            set 
            { 
                SetProperty(ref _printerIPselectedItem, value);
                EventUsePrinterIP = _printerIPselectedItem;
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
        /// 选择的打印机Model
        /// </summary>
        private PrinterModel _selectedItem;
        public PrinterModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                DriverInstallCommand = _selectedItem.PrinterDriverInstallCMD;
                DriverInstallCommandWorkDir = GlobalVar.GlobalDownloadPrinterExtrFolder(_selectedItem.PrinterDriverName);
                EventUsePrinterName = _selectedItem.PrinterName;
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

      

        public RelayCommand<FrameworkElement> ShowTextCmd => new(ShowText);

        private void ShowText(FrameworkElement element)
        {
            Dialog.Show(new TextDialog("Test..."));
        }



        public RelayCommand StartInstallPrinter => new(StartInstallPrinterCMD);
        private async void StartInstallPrinterCMD()
        {
            if (PrinterIPselectedItem != null)
                PrinterTools.AddMonitorPrinterPort(PrinterIPselectedItem, PrinterIPselectedItem, "Standard TCP/IP Port");
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
                DriverDownloadProgress = Convert.ToInt32(p.Progress);
                StartInstallBtnConntent = "下载中";
                if (p.Progress >= 100)
                {
                    StartInstallBtnConntent = "下载完成，请稍后";
                }
            });
            StartInstallFTPStatus = await tool.DownFileAsync(GlobalVar.GlobalDownloadPrinterFolder + "\\" + _selectedItem.PrinterDriverName,
                _selectedItem.PrinterDriverName, progress);
        }

        public RelayCommand OnlyDownloadDriver => new(OnlyDownloadDriverCMD);
        private async void OnlyDownloadDriverCMD()
        {
            if (PrinterIPselectedItem != null)
                PrinterTools.AddMonitorPrinterPort(PrinterIPselectedItem, PrinterIPselectedItem, "Standard TCP/IP Port");
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
                DriverDownloadProgress = Convert.ToInt32(p.Progress);
                ODLDriverBtnConntent = "下载中";
                if (p.Progress >= 100)
                {
                    ODLDriverBtnConntent = "下载完成，请稍后";
                }
            });
            ODLDriverFTPStatus = await tool.DownFileAsync(GlobalVar.GlobalDownloadPrinterFolder + "\\" + _selectedItem.PrinterDriverName,
                _selectedItem.PrinterDriverName, progress);
        }

        private event EventHandler<EventArgs> ODLEventHandler = async (s, e) =>
        {
            Console.WriteLine("解压完成");
            await CMDTools.SCAsyncInWorkDir(DriverInstallCommand, DriverInstallCommandWorkDir, ODLExitEvent);
            await Application.Current.Dispatcher.BeginInvoke((Action)(() =>
            {
                Dialog.Show(new TextDialog("解压完成，准备安装驱动\r\n请勿操作"));
            }));
           
        };
        private static event EventHandler ODLExitEvent = (s, e) =>
        {
            try
            {
                PrinterTools.InstallPrinterDriverFromPackage(pszDriverName: DriverInstallSystem, dwFlags: 0);
                App.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Dialog.Show(new TextDialog("安装完成\r\n请进行下一步"));
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
        private event EventHandler<EventArgs> StartInstallEventHandler = async (s, e) =>
        {
            Console.WriteLine("解压完成");
            await CMDTools.SCAsyncInWorkDir(DriverInstallCommand, DriverInstallCommandWorkDir, StartInstallExitEvent);
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                Dialog.Show(new TextDialog("解压完成，准备安装驱动\r\n请勿操作"));
            }));

        };
        private static event EventHandler StartInstallExitEvent = (s, e) =>
        {
            PrinterTools.InstallPrinterDriverFromPackage(pszDriverName: DriverInstallSystem, dwFlags: 0);
            PrintServer printServer = new(PrintSystemDesiredAccess.AdministrateServer);
            string[] port = new string[] { EventUsePrinterIP };
            try
            {
                var printQueue = printServer.InstallPrintQueue(EventUsePrinterName, DriverInstallSystem, port, "WinPrint", PrintQueueAttributes.Direct);
                Console.WriteLine(printQueue.Name);
                PrinterTools.SetDefaultPrinter(printQueue.Name);
                PrinterTools.PrintTestPage(null, null);
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Dialog.Show(new TextDialog("安装完成，已打印测试页\r\n请进行下一步"));
                }));
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    Dialog.Show(new TextDialog(ex.ToString()));
                }));
            }
        };
    }
}
