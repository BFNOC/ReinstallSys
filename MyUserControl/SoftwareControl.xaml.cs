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

        public static readonly DependencyProperty SoftwareDescriptionProperty = DependencyProperty.Register(
            "SoftwareDescription", typeof(string), typeof(SoftwareControl));

        public static readonly DependencyProperty SoftwareProgressBarProperty = DependencyProperty.Register(
            "SoftwareProgressBar", typeof(int), typeof(SoftwareControl));



        public string SoftwareName 
        {
            get => (string)GetValue(SoftwareNameProperty);
            set => SetValue(SoftwareNameProperty, value);
        }

        public string SoftwareDescription 
        {
            get => (string)GetValue(SoftwareDescriptionProperty);
            set => SetValue(SoftwareDescriptionProperty, value);
        }

        public int SoftwareProgressBar 
        {
            get => (int)GetValue(SoftwareProgressBarProperty);
            set => SetValue(SoftwareProgressBarProperty, value);
        }
    }
}
