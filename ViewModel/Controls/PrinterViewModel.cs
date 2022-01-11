using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data.Model;
using ReinstallSys.MyUserControl;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ReinstallSys.ViewModel.Controls
{
    public class PrinterViewModel : ViewModelBase<PrinterModel>
    {
        public PrinterViewModel(DataService dataService) => DataList = dataService.GetPrinterList();
        public List<IntPtr> PrinterList = new();


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
            
        private void StartInstallPrinterCMD()
        {
            if(SelectedItem != null)
            {
                if (HasRemoveAllPrinter)
                    if (!PrinterTools.DeletePrinterFromList(PrinterTools.GetPrinterIntPtrList()))
                        Dialog.Show(new TextDialog("出现未能删除旧打印机的错误"));
                Console.WriteLine(SelectedItem);
                Console.WriteLine("test:" + SelectedItem.PrinterName);
            } 
            else
            {
                Dialog.Show(new TextDialog("未选择打印机型号"));
            }
        }

        
    }
}
