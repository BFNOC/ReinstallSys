using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.ViewModel.Controls
{
    public class OfficeUninstallViewModel : ViewModelBase<OfficeUninstallModel>
    {
        public OfficeUninstallViewModel(DataService dataService) => DataList = dataService.GetOfficeUninstallList();

        private string _name;
        private string _fileURI;
        private string _uninstall_command;
        private string _uninstall_arguments;
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

        public string Uninstall_command
        {
            get => _uninstall_command;
            set => SetProperty(ref _uninstall_command, value);
        }
        public string Uninstall_arguments
        {
            get => _uninstall_arguments;
            set => SetProperty(ref _uninstall_arguments, value);
        }
    }
}
