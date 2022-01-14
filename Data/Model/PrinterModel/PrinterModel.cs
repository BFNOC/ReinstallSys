using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data.Model.PrinterModel
{
    public class PrinterModel
    {
        [JsonProperty("office_address")]
        public string OfficeAddress { get; set; }
        [JsonProperty("printer_name")]
        public string PrinterName { get; set; }
        [JsonProperty("printer_driver_name")]
        public string PrinterDriverName { get; set; }
        [JsonProperty("printer_driver_install_cmd")]
        public string PrinterDriverInstallCMD { get; set; }
        [JsonProperty("printer_driver_win7")]
        public string PrinterDriverWin7 { get; set; }
        [JsonProperty("printer_driver_win10")]
        public string PrinterDriverWin10 { get; set; }
        [JsonProperty("printer_ip")]
        public string PrinterIP { get; set; }
        [JsonProperty("update_time")]
        public string UpdateTime { get; set; }
        
    }
}
