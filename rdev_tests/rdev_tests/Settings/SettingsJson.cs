using Newtonsoft.Json;

namespace rdev_tests.Settings
{
    public class SettingsJson
    {
        [JsonProperty("rdev")]
        public RdevSettings Rdev { get; set; }
    }
}
