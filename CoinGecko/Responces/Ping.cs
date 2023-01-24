using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoinGecko.Responces
{
    internal class Ping
    {
        [JsonPropertyName("gecko_says")]
        public string? GeckoSays { get; set; }
    }
}
