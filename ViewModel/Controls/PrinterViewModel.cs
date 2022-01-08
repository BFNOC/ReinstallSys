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

        private bool _hasRemoveAllPrinter = true;
        public bool HasRemoveAllPrinter
        { 
            get => _hasRemoveAllPrinter;
            set => SetProperty(ref _hasRemoveAllPrinter, value);
        }

        public RelayCommand<FrameworkElement> ShowTextCmd => new(ShowText);

        private void ShowText(FrameworkElement element)
        {
            Dialog.Show(new TextDialog());
        }



        public RelayCommand Delete => new(DeleteCMD);
            
        private void DeleteCMD()
        {
            if(HasRemoveAllPrinter)
                if (!PrinterTools.DeletePrinterFromList(PrinterTools.GetPrinterIntPtrList()))
                    //MessageBox.Show("出现未能删除旧打印机的错误");
                    Dialog.Show(new TextDialog());
        }
    }
}
