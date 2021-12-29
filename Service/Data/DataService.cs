using ReinstallSys.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    Header = "步骤",
                    Description = "部署前准备"
                },

                new StepBarModel
                { 
                    Header = "步骤",
                    Description = "软件安装"
                },
                new StepBarModel
                {
                    Header = "步骤",
                    Description = "Office安装"
                }
            };
        }
    }
}
