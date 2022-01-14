using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.ViewModel.Controls.PrinterViewModel
{
    public class PrinterCustomModelViewModel : ObservableObject
    {

        public RelayCommand ClearPrinterSpool => new(ClearPrinterSpoolCMD);

        private void ClearPrinterSpoolCMD()
        {
            ProcessTools.Taskkill("spoolsv.exe");
            ProcessTools.Taskkill("printfilterpipelinesvc.exe");
            PrinterTools.ClearSpoolPrinters();
        }
    }
}
