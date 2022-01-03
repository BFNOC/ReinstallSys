using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data.Model
{
    public class OfficeUninstallModel
    {
        public string Name { get; set; }
        public string FileURL { get; set; }
        public string Uninstall_command { get; set; }
        public string Uninstall_arguments { get; set; }
    }
}
