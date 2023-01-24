using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.ViewModels;

public class ShellViewModel : Screen
{
	private readonly ICoinGeckoApiClient _coinGeckoApiClient;

    public ShellViewModel(ICoinGeckoApiClient coinGeckoApiClient)
	{
        _coinGeckoApiClient = coinGeckoApiClient;
        Top10Coins = new BindableCollection<CoinsMarkets>(_coinGeckoApiClient.GetCoinsMarkets().Result.Take(10));
    }

    public BindableCollection<CoinsMarkets> Top10Coins { get; set; } 
}
