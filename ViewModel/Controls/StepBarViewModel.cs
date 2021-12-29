using HandyControl.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using System.Linq;
using System.Windows.Controls;

namespace ReinstallSys.ViewModel.Controls
{
    public class StepBarViewModel : ViewModelBase<StepBarModel>
    {
        public StepBarViewModel(DataService dataService) => DataList = dataService.GetStepBarDataList();

        private int _stepIndex;
        public int StepIndex
        {
            get => _stepIndex;
            set => _stepIndex = value;
        }

        /// <summary>
        ///     下一步
        /// </summary>
        public RelayCommand<Panel> NextCmd => new(Next);

        /// <summary>
        ///     上一步
        /// </summary>
        public RelayCommand<Panel> PrevCmd => new(Prev);

        private void Next(Panel panel)
        {
            foreach (var stepBar in panel.Children.OfType<StepBar>())
            {
                stepBar.Next();
            }
        }

        private void Prev(Panel panel)
        {
            foreach (var stepBar in panel.Children.OfType<StepBar>())
            {
                stepBar.Prev();
            }
        }

    }
}
