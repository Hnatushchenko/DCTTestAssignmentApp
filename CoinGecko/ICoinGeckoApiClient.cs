using CoinGecko.Responces;

namespace CoinGecko
{
    public interface ICoinGeckoApiClient
    {
        Task<IReadOnlyCollection<CoinsMarkets>> GetCoinsMarkets(string currency = "usd");
    }
}