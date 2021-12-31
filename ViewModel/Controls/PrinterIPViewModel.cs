using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.ViewModel.Controls
{
    public class PrinterIPViewModel : ViewModelBase<PrinterIPModel>
    {
        public PrinterIPViewModel(DataService dataService) => DataList = dataService.GetPrinterIPList();

        private IPAddress _IPAddress;
        public IPAddress IPAddress
        {
            get => _IPAddress;
            set => SetProperty(ref _IPAddress, value);
        }
    }
}
