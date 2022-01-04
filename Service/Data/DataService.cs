using ReinstallSys.Data.Model;
using ReinstallSys.Tools;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ReinstallSys.Service.Data
{
    public class DataService
    {
        public List<StepBarModel> GetStepBarDataList()
        {
            return new()
            {
                new StepBarModel
                {
                    Header = "1",
                    Description = "部署前准备"
                },
                new StepBarModel
                {
                    Header = "2",
                    Description = "打印机安装"
                },
                new StepBarModel
                {
                    Header = "3",
                    Description = "Office安装"
                },
                new StepBarModel
                { 
                    Header = "4",
                    Description = "软件安装"
                },
                new StepBarModel
                {
                    Header = "5",
                    Description = "部署完成"
                }
            };
        }

        public List<BeforDeploymentModel> GetBeforDeploymentsList()
        {
            return new()
            {
                new BeforDeploymentModel
                { 
                    Description = "正在进行部署前准备"
                },
                new BeforDeploymentModel
                {
                    Description = "请勿操作计算机或断开电源"
                }
            };
        }

        public List<SoftwareModel> GetSoftwaresList()
        {
            var list = WebTools.GetSoftwareListFromWeb("http://bazx.mymiku.net/DontNet/ReinstallSys/Software.json");
            return list;
        }

        public List<PrinterModel> GetPrinterList()
        {
            var list = WebTools.GetPrinterListFromWeb("http://bazx.mymiku.net/DontNet/ReinstallSys/Printer.json");
            return list;
        }

        public List<PrinterIPModel> GetPrinterIPList()
        {
            var list = WebTools.GetPrinterIPListFromWeb("http://bazx.mymiku.net/DontNet/ReinstallSys/PrinterIP.json");
            return list;
        }

        public List<OfficeInstallModel> GetOfficeInstallList()
        {

            var list = WebTools.GetOfficeInstallListFromWeb("http://bazx.mymiku.net/DontNet/ReinstallSys/OfficeInstall.json");
            return list;
        }

        public List<OfficeUninstallModel> GetOfficeUninstallList()
        {
            return new()
            {
                new OfficeUninstallModel
                {
                    Name = "卸载Office 2010",
                    FileURL = "xxx",
                    Uninstall_command = "xxxx",
                    Uninstall_arguments = "/xxx"
                }
            };
        }
    }
}
