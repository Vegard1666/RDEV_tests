using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace rdev_tests.Settings
{
    public class RdevSettings
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }


    }
}
