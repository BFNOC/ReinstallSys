using FluentFTP;
using ReinstallSys.Data;
using ReinstallSys.Data.Model;
using ReinstallSys.Service.Data;
using ReinstallSys.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// SoftwareInstall.xaml 的交互逻辑
    /// </summary>
    public partial class SoftwareInstall : UserControl
    {
        DataService dataService = new();
        private List<SoftwareModel> ChangyongList;
        private List<SoftwareModel> OfficeList;
        private List<SoftwareModel> PlayerList;
        private List<SoftwareModel> SafeList;
        private List<SoftwareModel> InputList;
        private List<SoftwareModel> ZIPList;
        private List<SoftwareModel> BroswerList;
        private List<SoftwareModel> OtherList;
        private List<SoftwarePackageModel> PackageList;
        private List<SoftwareModel> WillDownloadSoft = new();

        private SoftwareModel softwareModel;
        private FtpStatus _status;
        public FtpStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                if (_status == FtpStatus.Success)
                {
                    
                    if (softwareModel.Name.Contains("*"))
                    {
                        CMDTools.SCAsyncInWorkDir(softwareModel.AutoInstall, GlobalVar.GlobalDownloadSoftwareFolder);
                        MessageBox.Show(softwareModel.AutoInstall);
                    }
                    else
                    {
                        CMDTools.SCAsyncInWorkDir(softwareModel.ManualInstall, GlobalVar.GlobalDownloadSoftwareFolder);
                        MessageBox.Show(softwareModel.ManualInstall);
                    }
                }
            }
        }
        public SoftwareInstall()
        {
            InitializeComponent();
            PackageList = WebTools.GetSoftwarePackageFromWeb("http://bazx.mymiku.net/DontNet/ReinstallSys/SoftwarePackage.json");
            PackageCombox.ItemsSource = PackageList;
            ChangyongList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "常用");
            OfficeList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "教师办公");
            PlayerList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "影音");
            SafeList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "安全");
            InputList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "输入法");
            ZIPList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "解压缩");
            BroswerList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "浏览器");
            OtherList = dataService.GetSoftwaresList().FindAll(SoftwareItem => SoftwareItem.Category == "其他");
            ChangyongItemsControl.ItemsSource = ChangyongList;
            OfficeItemsControl.ItemsSource = OfficeList;
            PlayerItemsControl.ItemsSource = PlayerList;
            SafeItemsControl.ItemsSource = SafeList;
            InputItemsControl.ItemsSource = InputList;
            ZIPItemsControl.ItemsSource = ZIPList;
            BroswerItemsControl.ItemsSource = BroswerList;
            OtherItemsControl.ItemsSource = OtherList;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var ftp = DataService.GetFTPModel();
            FTPTools tool = new(ftp.FTPAddress, ftp.SoftwareUsername, ftp.SoftwarePassword);
            foreach (var item in WillDownloadSoft)
            {
                Progress<FtpProgress> progress = new(p =>
                {
                    item.SoftwareProgressBar = Convert.ToInt32(p.Progress);
                    if (p.Progress >= 100)
                    {
                        
                    }
                });
                softwareModel = item;
                Status = await tool.DownFileAsync(GlobalVar.GlobalDownloadSoftwareFolder + "\\" + item.URL,
                item.URL, progress);
                
            }
            
        }

        private void PackageCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearAllSlected();
            SoftwarePackageModel model = (SoftwarePackageModel)PackageCombox.SelectedItem;
            SelectedSoft(model.SoftwareName);
            foreach (var item in model.SoftwareName)
            {
                Console.WriteLine(item);
            }
        }

        private void ClearAllSlected()
        {
            for (int i = 0; i < ChangyongList.Count; i++)
            {
                ChangyongList[i].HasSelected = false;
            }
            for (int i = 0; i < OfficeList.Count; i++)
            {
                OfficeList[i].HasSelected = false;
            }
            for (int i = 0; i < PlayerList.Count; i++)
            {
                PlayerList[i].HasSelected = false;
            }
            for (int i = 0; i < SafeList.Count; i++)
            {
                SafeList[i].HasSelected = false;
            }
            for (int i = 0; i < InputList.Count; i++)
            {
                InputList[i].HasSelected = false;
            }
            for (int i = 0; i < ZIPList.Count; i++)
            {
                ZIPList[i].HasSelected = false;
            }
            for (int i = 0; i < BroswerList.Count; i++)
            {
                BroswerList[i].HasSelected = false;
            }
            for (int i = 0; i < OtherList.Count; i++)
            {
                OtherList[i].HasSelected = false;
            }
        }
        private void SelectedSoft(List<string> SoftwareName)
        {
            foreach (var item in SoftwareName)
            {
                for (int i = 0; i < ChangyongList.Count; i++)
                {
                    if (ChangyongList[i].Name == item)
                    {
                        ChangyongList[i].HasSelected = true;
                    }
                }
                for (int i = 0; i < OfficeList.Count; i++)
                {
                    if(OfficeList[i].Name == item)
                    {
                        OfficeList[i].HasSelected = true;
                    }
                }
                for (int i = 0; i < PlayerList.Count; i++)
                {
                    if (PlayerList[i].Name == item)
                    {
                        PlayerList[i].HasSelected = true;
                    }
                }
                for (int i = 0; i < SafeList.Count; i++)
                {
                    if (SafeList[i].Name == item)
                    {
                        SafeList[i].HasSelected = true;
                    }
                }
                for (int i = 0; i < InputList.Count; i++)
                {
                    if (InputList[i].Name == item)
                    {
                        InputList[i].HasSelected = true;
                    }
                }
                for (int i = 0; i < ZIPList.Count; i++)
                {
                    if (ZIPList[i].Name == item)
                    {
                        ZIPList[i].HasSelected = true;
                    }
                }
                for (int i = 0; i < BroswerList.Count; i++)
                {
                    if (BroswerList[i].Name == item)
                    {
                        BroswerList[i].HasSelected = true;
                    }
                }
                for (int i = 0; i < OtherList.Count; i++)
                {
                    if(OtherList[i].Name == item)
                    {
                        OtherList[i].HasSelected = true;
                    }
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var s = sender as CheckBox;
            WillDownloadSoft.Add((SoftwareModel)s.DataContext);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var s = sender as CheckBox;
            WillDownloadSoft.Remove((SoftwareModel)s.DataContext);
        }
    }
}
