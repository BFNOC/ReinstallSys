using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data.Model
{
    public class PrinterIPModel
    {
        [JsonProperty("printer_ip")]
        public string PrinterIP { get; set; }
    }
}
