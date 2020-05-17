using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CovidNepalVisualization
{
    public partial class JhuModel
    {
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("province")]
        public string[] Province { get; set; }

        [JsonProperty("timeline")]
        public Timeline Timeline { get; set; }
    }

    public partial class Timeline
    {
        [JsonProperty("cases")]
        public Dictionary<string, long> Cases { get; set; }

        [JsonProperty("deaths")]
        public Dictionary<string, long> Deaths { get; set; }

        [JsonProperty("recovered")]
        public Dictionary<string, long> Recovered { get; set; }
    }
}
