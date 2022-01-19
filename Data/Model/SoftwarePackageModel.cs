using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data.Model
{
    public class SoftwarePackageModel
    {
        public string PackageName { get; set; }
        public List<string> SoftwareName { get; set; }
    }
}
