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

  
        /// <summary>
        ///     下一步
        /// </summary>
        public RelayCommand<StepBar> NextCmd => new(Next);

        /// <summary>
        ///     上一步
        /// </summary>
        public RelayCommand<StepBar> PrevCmd => new(Prev);

        private void Next(StepBar stepBar)
        {

            //foreach (var stepBar in panel.Children.OfType<StepBar>())
            //{
            //    stepBar.Next();

            //}
            stepBar.Next();
            
        }

        private void Prev(StepBar stepBar)
        {
            //foreach (var stepBar in panel.Children.OfType<StepBar>())
            //{
            //    stepBar.Prev();
            //}
            stepBar.Prev();
        }

       

    }
}
