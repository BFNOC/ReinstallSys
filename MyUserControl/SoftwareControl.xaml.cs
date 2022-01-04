using System.Windows;
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
        }

        public static readonly DependencyProperty SoftwareNameProperty = DependencyProperty.Register(
            "SoftwareName", typeof(string), typeof(SoftwareControl));
            

        public string SoftwareName 
        {
            get => (string)GetValue(SoftwareNameProperty);
            set => SetValue(SoftwareNameProperty, value);
        }

        public string SoftwareDescription { get; set; }

        public int SoftwareProgressBar { get; set; }
    }
}
