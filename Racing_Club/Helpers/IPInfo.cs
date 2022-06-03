using Newtonsoft.Json;

namespace Racing_Club.Helpers;

public class IPInfo
{
    [JsonProperty("ip")] public string Ip { get; set; }

    [JsonProperty("hostname")] public string HostName { get; set; }

    [JsonProperty("city")] public string City { get; set; }

    [JsonProperty("region")] public string Region { get; set; }

    [JsonProperty("country")] public string Country { get; set; }

    [JsonProperty("loc")] public string Location { get; set; }

    [JsonProperty("org")] public string Org { get; set; }

    [JsonProperty("postal")] public string Postal { get; set; }

    [JsonProperty("timezone")] public string TimeZone { get; set; }
}