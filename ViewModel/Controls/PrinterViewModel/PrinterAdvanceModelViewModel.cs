using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data.Model;
using ReinstallSys.Data.Model.PrinterModel;
using ReinstallSys.MyUserControl;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Printing;
using System.Printing.IndexedProperties;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReinstallSys.ViewModel.Controls.PrinterViewModel
{
    public class PrinterAdvanceModelViewModel : ViewModelBase<PrinterModel>
    {
        public PrinterAdvanceModelViewModel(DataService dataService) 
        {
            DataList = dataService.GetPrinterList();
            PrinterIP = dataService.GetPrinterIPList();
        }
        public List<IntPtr> PrinterList = new();

        private List<string> _printerIP;
        public List<string> PrinterIP
        {
            get => _printerIP;
            set => SetProperty(ref _printerIP, value);
        }

        private string _printerIPselectedItem;
        public string PrinterIPselectedItem
        {
            get => _printerIPselectedItem;
            set => SetProperty(ref _printerIPselectedItem, value);
        }
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
            set => SetProperty(ref _selectedItem, value);
        }

      

        public RelayCommand<FrameworkElement> ShowTextCmd => new(ShowText);

        private void ShowText(FrameworkElement element)
        {
            Dialog.Show(new TextDialog("Test..."));
        }



        public RelayCommand StartInstallPrinter => new(StartInstallPrinterCMD);

       

        private async void StartInstallPrinterCMD()
        {
            //LocalPrintServer myLocalPrintServer = new(PrintSystemDesiredAccess.AdministrateServer);
            PrintServer printServer = new(PrintSystemDesiredAccess.AdministrateServer);
            

            string[] port = new string[] { "172.28.56.240" };

            try
            {
                printServer.InstallPrintQueue("HP LaserJet MFP M227-M231", "HP LaserJet MFP M227-M231 PCL-6 (V4)", port, "WinPrint", PrintQueueAttributes.Direct);
                //myLocalPrintServer.InstallPrintQueue("test", "Microsoft Print To PDF", port, "WinPrint", PrintQueueAttributes.Direct);
                //myLocalPrintServer.Commit();
            }
            catch (Exception ex)
            {
                Dialog.Show(new TextDialog(ex.ToString()));
            }
            //Console.WriteLine(PrinterIPselectedItem.PrinterIP);
            //await CMDTools.SCAsync(@"msiexec /i C:\Users\User\Desktop\LJM227-M231_UWWL_4-1_UWWL_4_1_Full_WebPack_44.5.2693\LM227x64.msi /q");

            //if(SelectedItem != null)
            //{
            //    if (HasRemoveAllPrinter)
            //        if (!PrinterTools.DeletePrinterFromList(PrinterTools.GetPrinterIntPtrList()))
            //            Dialog.Show(new TextDialog("出现未能删除旧打印机的错误"));
            //} 
            //else
            //{
            //    Dialog.Show(new TextDialog("未选择打印机型号"));
            //}
        }


    }
}
