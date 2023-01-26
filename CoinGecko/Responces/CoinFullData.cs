using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static System.Net.Mime.MediaTypeNames;

namespace CoinGecko.Responces;

#nullable disable

public class CoinFullData
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("platforms")]
    public Dictionary<string, string> Platforms { get; set; }

    [JsonProperty("image")] public Image Image { get; set; }

    [JsonProperty("community_data", NullValueHandling = NullValueHandling.Ignore)]
    public CommunityData CommunityData { get; set; }

    [JsonProperty("developer_data", NullValueHandling = NullValueHandling.Ignore)]
    public DeveloperData DeveloperData { get; set; }

    [JsonProperty("public_interest_stats", NullValueHandling = NullValueHandling.Ignore)]
    public PublicInterestStats PublicInterestStats { get; set; }

    [JsonProperty("last_updated", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? LastUpdated { get; set; }

    [JsonProperty("localization", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, string> Localization { get; set; }

    [JsonProperty("market_data", NullValueHandling = NullValueHandling.Ignore)]
    public MarketData MarketData { get; set; }

    [JsonProperty("tickers")]
    public List<Ticket> Tickets { get; set; }
}

public class DeveloperData
{
    [JsonProperty("forks")] public long? Forks { get; set; }

    [JsonProperty("stars")] public long? Stars { get; set; }

    [JsonProperty("subscribers")] public long? Subscribers { get; set; }

    [JsonProperty("total_issues")] public long? TotalIssues { get; set; }

    [JsonProperty("closed_issues")] public long? ClosedIssues { get; set; }

    [JsonProperty("pull_requests_merged")] public long? PullRequestsMerged { get; set; }

    [JsonProperty("pull_request_contributors")]
    public long? PullRequestContributors { get; set; }

    [JsonProperty("code_additions_deletions_4_weeks", NullValueHandling = NullValueHandling.Ignore)]
    public Dictionary<string, long?> CodeAdditionsDeletions4Weeks { get; set; }

    [JsonProperty("commit_count_4_weeks")] public long? CommitCount4Weeks { get; set; }

    [JsonProperty("last_4_weeks_commit_activity_series", NullValueHandling = NullValueHandling.Ignore)]
    public long[] Last4WeeksCommitActivitySeries { get; set; }
}

public class MarketData
{
    [JsonProperty("market_cap_rank")]
    public long? MarketCapRank { get; set; }

    [JsonProperty("price_change_24h")]
    public decimal? PriceChange24H { get; set; }

    [JsonProperty("price_change_percentage_24h")]
    public double? PriceChangePercentage24H { get; set; }

    [JsonProperty("market_cap_change_24h")]
    public decimal? MarketCapChange24H { get; set; }

    [JsonProperty("market_cap_change_percentage_24h")]
    public decimal? MarketCapChangePercentage24H { get; set; }

    [JsonProperty("circulating_supply")]
    public string CirculatingSupply { get; set; }

    [JsonProperty("total_supply")]
    public decimal? TotalSupply { get; set; }

    [JsonProperty("roi")] public Roi Roi { get; set; }

    [JsonProperty("current_price")] public Dictionary<string, decimal?> CurrentPrice { get; set; }

    [JsonProperty("market_cap")] public Dictionary<string, decimal?> MarketCap { get; set; }

    [JsonProperty("total_volume")] public Dictionary<string, decimal?> TotalVolume { get; set; }

    [JsonProperty("high_24h")] public Dictionary<string, decimal?> High24H { get; set; }

    [JsonProperty("low_24h")] public Dictionary<string, decimal?> Low24H { get; set; }

    [JsonProperty("price_change_percentage_7d")]
    public string PriceChangePercentage7D { get; set; }

    [JsonProperty("price_change_percentage_14d")]
    public string PriceChangePercentage14D { get; set; }

    [JsonProperty("price_change_percentage_30d")]
    public string PriceChangePercentage30D { get; set; }

    [JsonProperty("price_change_percentage_60d")]
    public string PriceChangePercentage60D { get; set; }

    [JsonProperty("price_change_percentage_200d")]
    public string PriceChangePercentage200D { get; set; }

    [JsonProperty("price_change_percentage_1y")]
    public string PriceChangePercentage1Y { get; set; }

    [JsonProperty("price_change_24h_in_currency")]
    public Dictionary<string, decimal> PriceChange24HInCurrency { get; set; }

    [JsonProperty("price_change_percentage_1h_in_currency")]
    public Dictionary<string, double> PriceChangePercentage1HInCurrency { get; set; }

    [JsonProperty("price_change_percentage_24h_in_currency")]
    public Dictionary<string, double> PriceChangePercentage24HInCurrency { get; set; }

    [JsonProperty("price_change_percentage_7d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage7DInCurrency { get; set; }

    [JsonProperty("price_change_percentage_14d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage14DInCurrency { get; set; }

    [JsonProperty("price_change_percentage_30d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage30DInCurrency { get; set; }

    [JsonProperty("price_change_percentage_60d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage60DInCurrency { get; set; }

    [JsonProperty("price_change_percentage_200d_in_currency")]
    public Dictionary<string, double> PriceChangePercentage200DInCurrency { get; set; }

    [JsonProperty("price_change_percentage_1y_in_currency")]
    public Dictionary<string, double> PriceChangePercentage1YInCurrency { get; set; }

    [JsonProperty("market_cap_change_24h_in_currency")]
    public Dictionary<string, decimal> MarketCapChange24HInCurrency { get; set; }

    [JsonProperty("market_cap_change_percentage_24h_in_currency")]
    public Dictionary<string, decimal> MarketCapChangePercentage24HInCurrency { get; set; }
}

public class CommunityData
{
    [JsonProperty("facebook_likes")] public double? FacebookLikes { get; set; }

    [JsonProperty("twitter_followers")] public double? TwitterFollowers { get; set; }

    [JsonProperty("reddit_average_posts_48h")]
    public double? RedditAveragePosts48H { get; set; }

    [JsonProperty("reddit_average_comments_48h")]
    public double? RedditAverageComments48H { get; set; }

    [JsonProperty("reddit_subscribers")] public double? RedditSubscribers { get; set; }

    [JsonProperty("reddit_accounts_active_48h")]
    public double? RedditAccountsActive48H { get; set; }

    [JsonProperty("telegram_channel_user_count")]
    public double? TelegramChannelUserCount { get; set; }
}

public class PublicInterestStats
{
    [JsonProperty("alexa_rank")] public long? AlexaRank { get; set; }

    [JsonProperty("bing_matches")] public long? BingMatches { get; set; }
}

public class Image
{
    [JsonProperty("thumb")]
    public Uri Thumb { get; set; }

    [JsonProperty("small")]
    public Uri Small { get; set; }

    [JsonProperty("large")]
    public Uri Large { get; set; }
}

public class Ticket
{
    [JsonProperty("base")]
    public string Base { get; set; }

    [JsonProperty("target")]
    public string Target { get; set; }

    [JsonProperty("market")]
    public Market Market { get; set; }

    [JsonProperty("last")]
    public double Last { get; set; }

    [JsonProperty("volume")]
    public double Volume { get; set; }

    [JsonProperty("trust_score")]
    public string TrustScore { get; set; }

    [JsonProperty("trade_url")]
    public string TradeUrl { get; set; }
}

public class Market
{
    [JsonProperty("name")]
    public string Name { get; set; }

}