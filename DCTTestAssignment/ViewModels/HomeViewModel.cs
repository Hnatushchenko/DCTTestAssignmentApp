using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DCTTestAssignment.ViewModels
{
    public class HomeViewModel : Screen
    {
        private readonly ICoinGeckoApiClient _coinGeckoApiClient;
        private readonly SimpleContainer _container;
        private readonly ShellViewModel _shellViewModel;

        public HomeViewModel(ICoinGeckoApiClient coinGeckoApiClient, ShellViewModel shellViewModel, SimpleContainer container)
        {
            _coinGeckoApiClient = coinGeckoApiClient;
            _shellViewModel = shellViewModel;
            _container = container;
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
            set { _top10Coins = value; NotifyOfPropertyChange(() => Top10Coins); }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; NotifyOfPropertyChange(() => SearchText); }
        }

        public async Task OpenDetailsAsync(string id)
        {
            var detailsVM = _container.GetInstance<DetailsViewModel>();
            detailsVM.CryptocurrencyId = id;
            await _shellViewModel.ActivateItemAsync(detailsVM);
        }

        public async Task Search()
        {
            var coinList = await _coinGeckoApiClient.GetCoinListAsync();
            var coin = coinList.FirstOrDefault(coin => 
                coin.Name?.ToLower() == _searchText.ToLower());

            if (coin is null)
            {
                coin = coinList.FirstOrDefault(coin =>
                coin.Symbol?.ToLower() == _searchText.ToLower());
            }

            if (coin is not null && coin.Id is not null)
            {
                await OpenDetailsAsync(coin.Id);
            }
        }
    }
}
