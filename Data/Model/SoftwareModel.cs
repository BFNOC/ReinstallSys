using Newtonsoft.Json;
using System.ComponentModel;

namespace ReinstallSys.Data.Model
{
    public class SoftwareModel: INotifyPropertyChanged
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("URL")]
        public string URL { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        public string AutoInstall { get; set; }
        public string ManualInstall { get; set; }

        private int _softwareProgressBar;
        public int SoftwareProgressBar
        {
            get { return _softwareProgressBar; }
            set { _softwareProgressBar = value; Changed("SoftwareProgressBar"); }
        }

        private bool _hasSelected;
        public bool HasSelected
        {
            get { return _hasSelected; }
            set { _hasSelected = value; Changed("HasSelected"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void Changed(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
