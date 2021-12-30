using System.Collections.Generic;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ReinstallSys.ViewModel
{
    public class ViewModelBase<T> : ObservableObject
    {

        private IList<T> _dataList;

        public IList<T> DataList
        {
            //get { return _dataList; }
            //set { _dataList = value; }
            get => _dataList;
            set => SetProperty(ref _dataList, value);
        }

    }
}
