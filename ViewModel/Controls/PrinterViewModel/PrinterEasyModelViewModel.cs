using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data.Model.PrinterModel;
using ReinstallSys.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.ViewModel.Controls.PrinterViewModel
{
    public class PrinterEasyModelViewModel : ViewModelBase<PrinterModel>
    {
        public PrinterEasyModelViewModel(DataService dataService) => DataList = dataService.GetPrinterList();

        private bool _hasRemoveAllPrinter = false;
        public bool HasRemoveAllPrinter
        {
            get => _hasRemoveAllPrinter;
            set => SetProperty(ref _hasRemoveAllPrinter, value);
        }
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
                    + "打印机IP地址：" + _selectedItem.PrinterIP;
            }
        }

        private string _printerDetails;
        public string PrinterDetails
        {
            get => _printerDetails;
            set => SetProperty(ref _printerDetails, value);
        }

        public RelayCommand StartInstallPrinter => new(StartInstallPrinterCMD);



        private void StartInstallPrinterCMD()
        {
        }

        
    }
}
