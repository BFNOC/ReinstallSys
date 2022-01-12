using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data.Model;
using ReinstallSys.Data.Model.PrinterModel;
using ReinstallSys.Service.Data;
using ReinstallSys.Service.MyEnum;
using ReinstallSys.ViewModel.Controls.PrinterViewModel;
using System;
using System.Collections.Generic;

namespace ReinstallSys.ViewModel.Controls
{
    public class PrinterRootViewModel : ViewModelBase<PrinterUserControlModel>
    {
        public PrinterRootViewModel(DataService dataService) => DataList = dataService.GetPrinterUserControlList();

       

        private string _checknumber;

        private object _content;
        public object Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }

        

        public string Checknumber
        {
            get => _checknumber;
            set => SetProperty(ref _checknumber, value);
        }

        public RelayCommand<string> checkedq => new(new Action<string>(shows));

        private void shows(string name)
        {
            if (name == EnumService.PrinterRootEnum.简易模式.ToString())
            {
                Content = new PrinterEasyModelViewModel(new DataService());
            }
            else if (name == EnumService.PrinterRootEnum.高级模式.ToString())
            {
                Content = new PrinterAdvanceModelViewModel(new DataService());
            }
            else if(name == EnumService.PrinterRootEnum.自定义模式.ToString())
            {
                Content = new PrinterCustomModelViewModel();
            }
        }
    }
}
