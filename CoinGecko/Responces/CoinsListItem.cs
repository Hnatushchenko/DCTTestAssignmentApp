using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoinGecko.Responces
{
    public class CoinsListItem
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("symbol")]
        public string? Symbol { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("platforms")]
        public Dictionary<string, string>? Platforms { get; set; }
    }
}
