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
        private string _KMS_username;
        private string _KMS_password;

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
        [JsonProperty("KMS_username")]
        public string KMSUsername
        { 
            get { return _KMS_username; }
            set { _KMS_username = value; }
        }
        [JsonProperty("KMS_password")]
        public string KMSPassword
        {
            get { return _KMS_password; }
            set { _KMS_password = value; }
        }
    }
}
