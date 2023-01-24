using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoinGecko.Responces;

public class Roi
{
    [JsonPropertyName("times")] public decimal? Times { get; set; }

    [JsonPropertyName("currency")] public string? Currency { get; set; }

    [JsonPropertyName("percentage")] public decimal? Percentage { get; set; }
}