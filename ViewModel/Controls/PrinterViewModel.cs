using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.ViewModel.Controls
{
    public class PrinterViewModel : ViewModelBase<PrinterModel>
    {
        public PrinterViewModel(DataService dataService) => DataList = dataService.GetPrinterList();

        private string _printerName;
        public string PrinterName 
        { 
            get => _printerName;
            set => SetProperty(ref _printerName, value);
        }
    }
}
