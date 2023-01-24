using CoinGecko.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CoinGecko
{
    public class CoinGeckoApiClient : ICoinGeckoApiClient
    {
        private readonly HttpClient _httpClient;
        public CoinGeckoApiClient()
        {
            var socketHttpHandler = new SocketsHttpHandler()
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(1)
            };
            _httpClient = new HttpClient(socketHttpHandler)
            {
                BaseAddress = ApiEndPoints.BaseApiEndPoint
            };
        }

        public async Task<IReadOnlyCollection<CoinsMarkets>> GetCoinsMarkets()
        {
            var coinsMarketsList = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CoinsMarkets>>(ApiEndPoints.CoinsMarkets);

            if (coinsMarketsList is null)
            {
                throw new HttpRequestException("Responce is empty");
            }

            return coinsMarketsList!;
        }
    }
}
