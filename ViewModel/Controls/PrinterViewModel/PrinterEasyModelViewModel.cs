using FluentFTP;
using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data.Model.PrinterModel;
using ReinstallSys.MyUserControl;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using SevenZip;
using System;

namespace ReinstallSys.ViewModel.Controls.PrinterViewModel
{
    public class PrinterEasyModelViewModel : ViewModelBase<PrinterModel>
    {
        public PrinterEasyModelViewModel(DataService dataService) => DataList = dataService.GetPrinterList();
        protected string DownloadFolder = AppDomain.CurrentDomain.BaseDirectory + "\\PrinterDownload";
        private FtpStatus t;
        public FtpStatus T
        {
            get => t;
            set
            {
                t = value;
                if (t == FtpStatus.Success)
                {
                    Console.WriteLine("解压啊你");
                    SevenZipTools.UnZIPfile(DownloadFolder + "\\" + SelectedItem.PrinterDriverName,
                    DownloadFolder + "\\printer_extr", EventHandler);
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
            DirTools.CreateDir(DownloadFolder);
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
                    ButtonContent = "下载完成";
                }
            });
            T = await tool.DownFileAsync(DownloadFolder + "\\" + _selectedItem.PrinterDriverName, _selectedItem.PrinterDriverName, progress);
        }

        private EventHandler<FileInfoEventArgs> EventHandler = (s, e) =>
        {
            Dialog.Show(new TextDialog("解压完成"));
        };
        
    }
}
