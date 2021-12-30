using System.Windows.Controls;

namespace ReinstallSys.MyUserControl
{
    /// <summary>
    /// SoftwareControl.xaml 的交互逻辑
    /// </summary>
    public partial class SoftwareControl : UserControl
    {
        public SoftwareControl()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public string SoftwareName { get; set; }

        public string SoftwareDescription { get; set; }

        public int SoftwareProgressBar { get; set; }
    }
}
