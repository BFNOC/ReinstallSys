using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.ViewModel.Controls
{
    public class OfficeInstallViewModel : ViewModelBase<OfficeInstallModel>, INotifyPropertyChanged
    {

        public OfficeInstallViewModel(DataService dataService) => DataList = dataService.GetOfficeInstallList();
 





        private string _name;
        private string _fileURI;
        private string _installCommand;
        private string _uninstallCommand;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string FileURI
        {
            get => _fileURI;
            set => SetProperty(ref _fileURI, value);
        }
        public string InstallCommand
        {
            get => _installCommand;
            set => SetProperty(ref _installCommand, value);
        }
        public string UninstallCommand
        {
            get => _uninstallCommand;
            set => SetProperty(ref _uninstallCommand, value);
        }

    }
}
