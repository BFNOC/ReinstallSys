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

namespace ReinstallSys.UserController.Printer
{
    /// <summary>
    /// PrinterAdvancedModelUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PrinterAdvancedModelUserControl : UserControl
    {
        public PrinterAdvancedModelUserControl()
        {
            InitializeComponent();
            //PrinterTools.AddMonitorPrinterPort("172.28.56.240", "172.28.56.240", "Standard TCP/IP Port");
            //PrinterTools.InstallPrinterDriverFromPackage(pszDriverName: "HP LaserJet MFP M227-M231 PCL-6 (V4)",dwFlags:0);
        }
    }
}
