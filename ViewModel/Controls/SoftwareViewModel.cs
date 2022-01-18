using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace ReinstallSys.ViewModel.Controls
{
    public class SoftwareViewModel : ViewModelBase<SoftwareModel>
    {
        public SoftwareViewModel(DataService dataService) 
        { 
            DataList = dataService.GetSoftwaresList(); 
            
            foreach (var data in DataList)
            {
                if (!SoftwareDic.ContainsKey(data.Category))
                {
                    SoftwareDic.Add(data.Category, new List<SoftwareModel>());
                    SoftwareDic[data.Category].Add(data);
                }
                else
                {
                    SoftwareDic[data.Category].Add(data);
                }
                    
            }
        }
        
        private ObservableDictionary<string, List<SoftwareModel>> _softwareDic = new();
        public ObservableDictionary<string, List<SoftwareModel>> SoftwareDic
        {
            get => _softwareDic;
            set
            {
                _softwareDic = value;
                SetProperty(ref _softwareDic, value);
                
            }
        }

        private ObservableCollection<SoftwareModel> _softwareList = new();
        public ObservableCollection<SoftwareModel> SoftwareList { get => _softwareList; set => SetProperty(ref _softwareList, value); }

        public RelayCommand StartProcess => new(StartProcessCMD);
        public RelayCommand<object> SoftwareCheckCommand => new(SoftwareCheckCMD);


        private void StartProcessCMD()
        {
            foreach (var item in SoftwareList)
            {
                System.Console.WriteLine(item.Name);
                
            }
        }
        private void SoftwareCheckCMD(object parameter)
        {
            var values = (object[])parameter;
            var isChecked = (bool)values[0];
            var SoftwareModel = (SoftwareModel)values[1];
            if (isChecked)
            {
                SoftwareList.Add(SoftwareModel);
            }
            else
            {
                SoftwareList.Remove(SoftwareModel);
            }
        }
    }
}
