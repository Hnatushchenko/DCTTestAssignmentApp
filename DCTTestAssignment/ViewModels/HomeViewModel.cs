using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCTTestAssignment.ViewModels
{
    public class HomeViewModel : Screen
    {
        private readonly ICoinGeckoApiClient _coinGeckoApiClient;

        public HomeViewModel(ICoinGeckoApiClient coinGeckoApiClient)
        {
            _coinGeckoApiClient = coinGeckoApiClient;
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var coins = (await _coinGeckoApiClient.GetCoinsMarketsAsync()).Take(10);
            Top10Coins = new BindableCollection<CoinsMarkets>(coins);
        }

        public BindableCollection<CoinsMarkets> Top10Coins { get; set; } = null!;
    }
}
