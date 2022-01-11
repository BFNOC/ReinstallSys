using ReinstallSys.Structures;
using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace ReinstallSys.UserController
{
    /// <summary>
    /// PrinterInstall.xaml 的交互逻辑
    /// </summary>
    public partial class PrinterInstall : UserControl
    {
        public List<IntPtr> PrinterList = new();
        public PrinterInstall()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //PrinterTools.AddMonitorPrinterPort("172.28.56.240", "172.28.56.240", "Standard TCP/IP Port");
            //PrinterTools.InstallPrinterDriverFromPackage(pszDriverName: "HP LaserJet MFP M227-M231 PCL-6 (V4)",dwFlags:0);
            
        }
    }
}
