using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReinstallSys.Data.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReinstallSys.Tools
{
    internal class WebTools
    {
        
        public static string GetJSONFromUrl(string url)
        {
            try
            {
                var client = new WebClient();
                client.Encoding = Encoding.UTF8;
                var data = client.DownloadString(url);
                return data;
            }
            catch (HttpRequestException) // Non success
            {
                Console.WriteLine("An error occurred.");
            }
            catch (NotSupportedException) // When content type is not valid
            {
                Console.WriteLine("The content type is not supported.");
            }
            catch (JsonException) // Invalid JSON
            {
                Console.WriteLine("Invalid JSON.");
            }
            return "访问失败";
        }
        

        public static FTPModel GetFTPFromWeb(string url)
        {
            var data = GetJSONFromUrl(url);
            var output = JsonConvert.DeserializeObject<FTPModel>(data);
            return output;
        }

        public static List<OfficeInstallModel> GetOfficeInstallListFromWeb(string url)
        {
            var data = GetJSONFromUrl(url);
            var output = JsonConvert.DeserializeObject<List<OfficeInstallModel>>(data);
            return output;
        }
        public static List<PrinterModel> GetPrinterListFromWeb(string url)
        {
            var data = GetJSONFromUrl(url);
            var output = JsonConvert.DeserializeObject<List<PrinterModel>>(data);
            return output;
        }
        public static List<PrinterIPModel> GetPrinterIPListFromWeb(string url)
        {
            var data = GetJSONFromUrl(url);
            var output = JsonConvert.DeserializeObject<List<PrinterIPModel>>(data);
            return output;
        }

        public static List<SoftwareModel> GetSoftwareListFromWeb(string url)
        {
            var data = GetJSONFromUrl(url);
            var output = JsonConvert.DeserializeObject<List<SoftwareModel>>(data);
            return output;
        }
    }
}
