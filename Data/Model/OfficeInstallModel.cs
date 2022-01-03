using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data.Model
{
    public class OfficeInstallModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("file_URI")]
        public string FileURI { get; set; }
        [JsonProperty("install_command")]
        public string InstallCommand { get; set; }
        [JsonProperty("install_arguments")]
        public string InstallArguments { get; set; }
    }
}
