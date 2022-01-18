using System.Windows.Controls;

namespace ReinstallSys.MyUserControl
{
    /// <summary>
    /// MyTextDialog.xaml 的交互逻辑
    /// </summary>
    public partial class MyTextDialog : UserControl
    {
        public MyTextDialog(string info)
        {
            InitializeComponent();
            textBlock.Text = info;
        }
    }
}
