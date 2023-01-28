using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using DCTTestAssignment.Data;
using DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;
using DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;
using DCTTestAssignment.Data.ThemeSupport.ThemeData;
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
        private readonly ILocalizationDataProvider<IHomeViewLocalizationData> _localizationDataProvider;

        private IHomeViewLocalizationData? _localizationData;
        public IHomeViewLocalizationData? LocalizationData
        {
            get { return _localizationData; }
            set { _localizationData = value; NotifyOfPropertyChange(() => LocalizationData); }
        }

        private IThemeData? _themeData;
        public IThemeData? ThemeData
        {
            get { return _themeData; }
            set { _themeData = value; NotifyOfPropertyChange(() => ThemeData); }
        }

        public HomeViewModel(ICoinGeckoApiClient coinGeckoApiClient, SimpleContainer container, IEventAggregator eventAggregator, ILocalizationDataProvider<IHomeViewLocalizationData> localizationDataProvider)
        {
            _coinGeckoApiClient = coinGeckoApiClient;
            _container = container;
            eventAggregator.SubscribeOnUIThread(this);
            _localizationDataProvider = localizationDataProvider;
        }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var coins = await _coinGeckoApiClient.GetCoinsMarketsAsync();
            Top10Coins = new ObservableCollection<CoinsMarkets>(coins);
        }

        private ObservableCollection<CoinsMarkets> _top10Coins = new();
        public ObservableCollection<CoinsMarkets> Top10Coins
        {
            get { return _top10Coins; }
            set { _top10Coins = value; NotifyOfPropertyChange(() => Top10Coins); }
        }

        private string _searchText = "";
        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; NotifyOfPropertyChange(() => SearchText); }
        }

        public async Task OpenDetailsAsync(string id)
        {
            var shellViewModel = _container.GetInstance<ShellViewModel>();
            await shellViewModel.OpenDetailsViewAsync(id);
        }

        public void UpdateLanguage(string language)
        {
            LocalizationData = _localizationDataProvider.GetLocalizationData(language);
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
