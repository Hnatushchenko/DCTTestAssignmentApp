using CoinGecko.Responces;

namespace CoinGecko
{
    public interface ICoinGeckoApiClient
    {
        Task<IReadOnlyCollection<CoinsMarkets>> GetCoinsMarketsAsync(string currency = "usd");
        Task<CoinFullData> GetCoinFullDataAsync(string id);
        Task<IReadOnlyCollection<CoinsListItem>> GetCoinListAsync();
        Task<decimal?> GetCoinPriceInUSDByNameOrSymbol(string identifier);
    }
}