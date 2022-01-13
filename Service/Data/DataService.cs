using ReinstallSys.Data.Model;
using ReinstallSys.Data.Model.PrinterModel;
using ReinstallSys.Service.MyEnum;
using ReinstallSys.Tools;
using System;
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

        public List<string> GetPrinterIPList()
        {
            var list = WebTools.GetPrinterListFromWeb("http://bazx.mymiku.net/DontNet/ReinstallSys/Printer.json");
            Console.WriteLine(list);
            List<string> IPList = new();
            foreach (var item in list)
            {
                IPList.Add(item.PrinterIP);
            }
            return IPList;
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

        public List<PrinterUserControlModel> GetPrinterUserControlList()
        {
            List<PrinterUserControlModel> list = new();
            foreach (var i in Enum.GetNames(typeof(EnumService.PrinterRootEnum)))
            {
                list.Add(new PrinterUserControlModel() { Name = i.ToString() });
            }
            return list;
        }

        public static FTPModel GetFTPModel()
        {
            FTPModel ftpModel = WebTools.GetFTPFromWeb("http://bazx.mymiku.net/DontNet/ReinstallSys/ftp.json");
            return ftpModel;
        }

    }
}
