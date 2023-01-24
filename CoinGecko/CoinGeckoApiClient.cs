using CoinGecko.Responces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        public async Task<IReadOnlyCollection<CoinsMarkets>> GetCoinsMarkets(string currency = "usd")
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, ApiEndPoints.CoinsMarkets + $"?vs_currency={currency}&order=market_cap_desc&per_page=100&page=1&sparkline=false"))
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            var coinsMarketsList = JsonConvert.DeserializeObject<IReadOnlyCollection<CoinsMarkets>>(responseContent);

            if (coinsMarketsList is null)
            {
                throw new HttpRequestException("Responce is empty");
            }

            return coinsMarketsList;
        }
    }
}
