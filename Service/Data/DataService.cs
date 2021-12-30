using ReinstallSys.Data.Model;
using System.Collections.Generic;

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
                    Header = "步骤",
                    Description = "部署前准备"
                },
                new StepBarModel
                {
                    Header = "步骤",
                    Description = "打印机安装"
                },
                new StepBarModel
                {
                    Header = "步骤",
                    Description = "Office安装"
                },
                new StepBarModel
                { 
                    Header = "步骤",
                    Description = "软件安装"
                },
                new StepBarModel
                {
                    Header = "步骤",
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
    }
}
