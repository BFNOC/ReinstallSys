using ReinstallSys.Data.Model;
using System.Collections.Generic;
using System.Net;

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
            return new()
            {
                new SoftwareModel
                {
                    SoftwareName = "QQ",
                    SoftwareDescription = "通讯软件",
                    SoftwareProgressBar = 100
                }
            };
        }

        //TODO: get data from intranet
        public List<PrinterModel> GetPrinterList()
        {
            return new()
            {
                new PrinterModel
                {
                    PrinterName = "HP M203-M206DN"
                }
            };
        }

        //TODO: get data from intranet
        public List<PrinterIPModel> GetPrinterIPList()
        {
            return new()
            {
                new PrinterIPModel 
                { PrinterIP = IPAddress.Parse("1.1.1.1") }
            };
        }
    }
}
