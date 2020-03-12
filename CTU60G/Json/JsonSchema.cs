namespace CTU60G.Json
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class WirelessSite
    {
        [JsonProperty("infos", Required = Required.Always, NullValueHandling = NullValueHandling.Ignore)]
        public WirelessSiteInfo Infos { get; set; }

        [JsonProperty("stations", NullValueHandling = NullValueHandling.Ignore)]
        public List<WirelessUnit> Stations { get; set; }

        [JsonProperty("ap", NullValueHandling = NullValueHandling.Ignore)]
        public List<WirelessUnit> Ap { get; set; }
    }

    public partial class WirelessUnit
    {
        [JsonProperty("lat", NullValueHandling = NullValueHandling.Ignore)]
        public string Lat { get; set; }

        [JsonProperty("lon", NullValueHandling = NullValueHandling.Ignore)]
        public string Lon { get; set; }

        [JsonProperty("freq", NullValueHandling = NullValueHandling.Ignore)]
        public string Freq { get; set; }

        [JsonProperty("mac_addr", NullValueHandling = NullValueHandling.Ignore)]
        public string MacAddr { get; set; }

        [JsonProperty("mode", NullValueHandling = NullValueHandling.Ignore)]
        public string Mode { get; set; }

        [JsonProperty("ctu_reported", NullValueHandling = NullValueHandling.Ignore)]
        public string CtuReported { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
    }

    public partial class WirelessSiteInfo
    {
        [JsonProperty("ssid", NullValueHandling = NullValueHandling.Ignore)]
        public string Ssid { get; set; }

        [JsonProperty("subnet_id", NullValueHandling = NullValueHandling.Ignore)]
        public string SubnetId { get; set; }
    }
}