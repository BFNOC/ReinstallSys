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
        [JsonProperty("printer_ip")]
        public string PrinterIP { get; set; }
    }
}
