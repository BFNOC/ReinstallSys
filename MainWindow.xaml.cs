using ReinstallSys.UserController;
using ReinstallSys.ViewModel.Main;
using System.Windows;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void StepBar_StepChanged(object sender, HandyControl.Data.FunctionEventArgs<int> e)
        {
            System.Console.WriteLine("增加事件");
        }
    }
}
