using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;

namespace ReinstallSys.ViewModel.Controls
{
    public class BeforDeploymentViewModel : ViewModelBase<BeforDeploymentModel>
    {
        public BeforDeploymentViewModel(DataService dataService) => DataList = dataService.GetBeforDeploymentsList();

        private string _desc;
        public string Desc
        {
            get => _desc;
            set => SetProperty(ref _desc, value);
        }
        
    }
}
