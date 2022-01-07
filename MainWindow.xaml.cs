using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using ReinstallSys.UserController;
using ReinstallSys.ViewModel.Main;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace ReinstallSys
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel();
            DataContext = vm;
            myStepBar.StepBar.StepChanged += StepBar_StepChanged;
            
        }

        

        private void StepBar_StepChanged(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            mainContent.Children.Clear();
            foreach (StepBarModel item in myStepBar.StepBar.Items)
            {
                if (item.Header == (myStepBar.StepBar.StepIndex + 1).ToString())
                {
                    if (item.Description == "部署前准备")
                    {
                        var n = new BeforeDeployment();
                        n.SC.TextChanged += SC_TextChanged;
                        mainContent.Children.Add(n);
                    }
                    else if (item.Description == "打印机安装")
                    {
                        mainContent.Children.Add(new PrinterInstall());
                    }
                    else if (item.Description == "软件安装")
                    {
                        mainContent.Children.Add(new SoftwareInstall());
                    }
                    else if (item.Description == "Office安装")
                    {
                        mainContent.Children.Add(new OfficeInstall());
                    }
                }
            }
        }

        private void SC_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text == "即将进入下一步")
            {
                myStepBar.StepBar.Next();
            }
        }
    }
}
