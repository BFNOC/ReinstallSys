using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.MyUserControl;
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
            try
            {
                ProcessTools.Taskkill("spoolsv.exe");
                ProcessTools.Taskkill("printfilterpipelinesvc.exe");
                PrinterTools.ClearSpoolPrinters();
                Dialog.Show(new TextDialog("清除完成！"));
            }
            catch (Exception ex)
            {
                Dialog.Show(new TextDialog(ex.ToString()));
            }
            
        }
    }
}
