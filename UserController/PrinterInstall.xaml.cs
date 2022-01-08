using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
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
    }
}
