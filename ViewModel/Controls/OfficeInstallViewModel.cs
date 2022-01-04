using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using System.ComponentModel;
using System.Linq;

namespace ReinstallSys.ViewModel.Controls
{
    public class OfficeInstallViewModel : ViewModelBase<OfficeInstallModel>, INotifyPropertyChanged
    {
        protected string OperatingSystem;

        public OfficeInstallViewModel(DataService dataService)
        {
            DataList = dataService.GetOfficeInstallList();
            OperatingSystem = OSTools.GetOperatingSystemVersion();
            if (OperatingSystem == "Windows 7")
            {
                DataList.Remove(DataList.Single(item => item.Name == "Office 2019"));
                DataList.Remove(DataList.Single(item => item.Name == "Office 2021"));
            }
        }

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
