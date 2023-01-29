using CoinGecko.Responces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CoinGecko;

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

    private async Task<TData> GetDataAsync<TData>(string uri)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await _httpClient.SendAsync(httpRequestMessage)
            .ConfigureAwait(false);

        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        try
        {
            return JsonConvert.DeserializeObject<TData>(responseContent)
                ?? throw new ArgumentNullException(nameof(responseContent));
        }
        catch (Exception e)
        {
            throw new HttpRequestException(e.Message);
        }
    }

    public async Task<IReadOnlyCollection<CoinsMarkets>> GetCoinsMarketsAsync(string currency = "usd")
    {
        var uri = ApiEndPoints.CoinsMarkets + $"?vs_currency={currency}&order=market_cap_desc&per_page=100&page=1&sparkline=false";
        return await GetDataAsync<IReadOnlyCollection<CoinsMarkets>>(uri);
    }

    public async Task<CoinFullData> GetCoinFullDataAsync(string id)
    {
        var uri = ApiEndPoints.AllDataByCoinId(id) + "?localization=false";
        return await GetDataAsync<CoinFullData>(uri);
    }

    public async Task<IReadOnlyCollection<CoinsListItem>> GetCoinListAsync()
    {
        return await GetDataAsync<IReadOnlyCollection<CoinsListItem>>(ApiEndPoints.CoinsList);
    }

    public async Task<decimal?> GetCoinPriceInUSDByNameOrSymbol(string identifier)
    {
        var coinList = await GetCoinListAsync();
        var coin = coinList.FirstOrDefault(coin =>
            coin.Name?.ToLower() == identifier.ToLower());

        if (coin is null)
        {
            coin = coinList.FirstOrDefault(coin =>
            coin.Symbol?.ToLower() == identifier.ToLower());
        }

        if (coin is not null && coin.Id is not null)
        {
            var coinFullData = await GetCoinFullDataAsync(coin.Id);
            decimal price = coinFullData.MarketData.CurrentPrice["usd"] 
                ?? throw new HttpRequestException("Price was not provided by the CoinGecko API");
            
            return price;
        }
        return null;
    }
}
