using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var coins = (await _coinGeckoApiClient.GetCoinsMarketsAsync()).Take(10);
            Top10Coins = new ObservableCollection<CoinsMarkets>(coins);
        }

        private ObservableCollection<CoinsMarkets> _top10Coins = new();

        public ObservableCollection<CoinsMarkets> Top10Coins
        {
            get { return _top10Coins; }
            set
            {
                _top10Coins = value;
                NotifyOfPropertyChange(() => Top10Coins);
            }
        }
    }
}
