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

        public async Task<IReadOnlyCollection<CoinsListItem>?> GetCoinsList()
        {
            var coinsList = await _httpClient.GetFromJsonAsync<IReadOnlyCollection<CoinsListItem>>(ApiEndPoints.CoinsList);
            return coinsList;
        }
    }
}
