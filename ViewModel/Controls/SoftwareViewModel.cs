using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;

namespace ReinstallSys.ViewModel.Controls
{
    public class SoftwareViewModel : ViewModelBase<SoftwareModel>
    {
        public SoftwareViewModel(DataService dataService) => DataList = dataService.GetSoftwaresList();

        private SoftwareModel _SoftwareDataList;

        public SoftwareModel SoftwareDataList
        {
            get => _SoftwareDataList;
            set => _SoftwareDataList = value;
        }
    }
}
