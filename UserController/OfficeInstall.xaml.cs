using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReinstallSys.UserController
{
    /// <summary>
    /// OfficeInstall.xaml 的交互逻辑
    /// </summary>
    public partial class OfficeInstall : UserControl
    {
        
        public OfficeInstall()
        {
            InitializeComponent();
        }

        private void OfficeUninstallCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            UninstallOfficeVersion.SelectedIndex = -1;
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            InstallOfficeVersion.SelectedIndex = -1;
        }
    }
}
