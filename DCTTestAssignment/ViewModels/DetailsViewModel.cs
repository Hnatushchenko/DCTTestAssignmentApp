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
    private readonly string _cryptocurrencyId;
    private readonly ICoinGeckoApiClient _coinGeckoApiClient;

    public DetailsViewModel(string cryptocurrencyId, ICoinGeckoApiClient coinGeckoApiClient)
    {
        _cryptocurrencyId = cryptocurrencyId;
        _coinGeckoApiClient = coinGeckoApiClient;
    }

    protected override async Task OnActivateAsync(CancellationToken cancellationToken)
    {
        CoinFullData = await _coinGeckoApiClient.GetCoinFullDataAsync(_cryptocurrencyId);
    }

    private CoinFullData? _coinFullData;

    public CoinFullData? CoinFullData
    {
        get { return _coinFullData; }
        set
        {
            _coinFullData = value;
            NotifyOfPropertyChange(() => _coinFullData);
        }
    }
}
