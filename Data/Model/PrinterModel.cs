using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data.Model
{
    public class PrinterModel
    {
        [JsonProperty("printer_name")]
        public string PrinterName { get; set; }
        [JsonProperty("printer_driver_name")]
        public string PrinterDriverName { get; set; }
    }
}
