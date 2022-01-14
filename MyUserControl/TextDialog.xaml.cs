using System.Windows.Controls;

namespace ReinstallSys.MyUserControl
{
    /// <summary>
    /// TextDialog.xaml 的交互逻辑
    /// </summary>
    public partial class TextDialog : UserControl
    {
        public TextDialog(string info)
        {
            InitializeComponent();
            textBlock.Text = info;
        }
    }
}
