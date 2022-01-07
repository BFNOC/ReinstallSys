using HandyControl.Controls;
using ReinstallSys.Tools;
using ReinstallSys.ViewModel;
using ReinstallSys.ViewModel.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ReinstallSys.UserController
{
    /// <summary>
    /// BeforeDeployment.xaml 的交互逻辑
    /// </summary>
    public partial class BeforeDeployment : UserControl
    {
        private bool _LTSC2021;
        public List<string> MSVCLibs = new List<string>
        {
            @"C:\Users\User\Desktop\Microsoft.VCLibs.140.00_14.0.30704.0_x64__8wekyb3d8bbwe.Appx",
            @"C:\Users\User\Desktop\Microsoft.VCLibs.140.00_14.0.30704.0_x86__8wekyb3d8bbwe.Appx"
        };

        public bool LTSC2021
        {
            get { return _LTSC2021; }
            set { _LTSC2021 = value; }
        }
        public BeforeDeployment()
        {
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LTSC2021 = OSTools.CheckWhetherSystemIsLTSC2021();
            if (LTSC2021)
            {
                foreach (var i in MSVCLibs)
                {
                    SC.Text = "正在进行第" + (MSVCLibs.IndexOf(i) + 1).ToString() + "项/共" + MSVCLibs.Count.ToString() + "项";
                    await CMDTools.SCAsync("powershell Add-AppxPackage " + i);
                }
            }
            SC.Text = "即将进入下一步";
            
        }

        
    }
}
