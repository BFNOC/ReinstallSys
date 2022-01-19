using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReinstallSys.Data.Model
{
    public class FTPModel
    {
        private string _ftp_address;
        private string _office_username;
        private string _office_password;
        private string _printer_username;
        private string _printer_password;
        private string _software_username;
        private string _software_password;

        [JsonProperty("ftp_address")]
        public string FTPAddress
        {
            get { return _ftp_address; }
            set { _ftp_address = value; }
        }
        [JsonProperty("office_username")]
        public string OfficeUsername
        {
            get { return _office_username; }
            set { _office_username = value; }
        }
        [JsonProperty("office_password")]
        public string OfficePassword
        {
            get { return _office_password; }
            set { _office_password = value; }
        }
        [JsonProperty("printer_username")]
        public string PrinterUsername
        { 
            get { return _printer_username; }
            set { _printer_username = value; }
        }
        [JsonProperty("printer_password")]
        public string PrinterPassword
        {
            get { return _printer_password; }
            set { _printer_password = value; }
        }
        [JsonProperty("software_username")]
        public string SoftwareUsername
        {
            get { return _software_username; }
            set { _software_username = value; }
        }
        [JsonProperty("software_password")]
        public string SoftwarePassword
        {
            get { return _software_password; }
            set { _software_password = value; }
        }
    }
}
