using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoinGecko.Responces;

public class CoinsMarkets
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("symbol")]
    public string? Symbol { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("image")]
    public Uri? Image { get; set; }

    [JsonProperty("current_price")]
    public decimal? CurrentPrice { get; set; }

    [JsonProperty("market_cap")]
    public decimal? MarketCap { get; set; }

    [JsonProperty("market_cap_rank")]
    public int? MarketCapRank { get; set; }

    [JsonProperty("total_volume")]
    public decimal? TotalVolume { get; set; }

    [JsonProperty("high_24h")]
    public decimal? High24H { get; set; }

    [JsonProperty("low_24h")]
    public decimal? Low24H { get; set; }

    [JsonProperty("ath")]
    public decimal? Ath { get; set; }

    [JsonProperty("ath_change_percentage")]
    public decimal? AthChangePercentage { get; set; }

    [JsonProperty("ath_date")]
    public DateTimeOffset? AthDate { get; set; }

    [JsonProperty("roi")]
    public Roi? Roi { get; set; }

    [JsonProperty("last_updated")]
    public DateTimeOffset? LastUpdated { get; set; }
}
