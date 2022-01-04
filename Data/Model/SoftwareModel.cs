using Newtonsoft.Json;

namespace ReinstallSys.Data.Model
{
    public class SoftwareModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("URL")]
        public string URL { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        public string AutoInstall { get; set; }
        public string ManualInstall { get; set; }

        public int SoftwareProgressBar { get; set; }

    }
}
