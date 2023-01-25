using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCTTestAssignment.ViewModels;

public class DetailsViewModel : Screen
{
    private readonly ICoinGeckoApiClient _coinGeckoApiClient;

    public DetailsViewModel(ICoinGeckoApiClient coinGeckoApiClient)
    {
        _coinGeckoApiClient = coinGeckoApiClient;
    }

    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(CryptocurrencyId))
        {
            throw new Exception($"{nameof(CryptocurrencyId)} must be not null and not an empty string");
        }
        CoinFullData = await _coinGeckoApiClient.GetCoinFullDataAsync(CryptocurrencyId); //CoinFullData.MarketData.TotalVolume;
    }

    private CoinFullData? _coinFullData;

    public CoinFullData? CoinFullData
    {
        get { return _coinFullData; }
        set
        {
            _coinFullData = value;
            NotifyOfPropertyChange(() => CoinFullData);
        }
    }

    public string? CryptocurrencyId { get; set; }
}
