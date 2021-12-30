using ReinstallSys.Data.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.ViewModel.Main
{
    public class MainWindowViewModel : ViewModelBase<MainWindowModel>
    {
        private ObservableCollection<MainWindowModel> dataList;
        public new ObservableCollection<MainWindowModel> DataList
        {
            get => dataList;
            set => SetProperty(ref dataList, value);

        }

        private int selectedIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public MainWindowViewModel()
        {
            SelectedIndex = -1;
            DataList = GetDataList();
        }

        private ObservableCollection<MainWindowModel> GetDataList()
        {
            return new ObservableCollection<MainWindowModel>
            {
                new MainWindowModel
                {
                    Name = "软件安装"
                }
            };
        }
    }
}
