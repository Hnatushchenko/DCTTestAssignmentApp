using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using DCTTestAssignment.Data.LocalizationData;
using DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;
using DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;
using DCTTestAssignment.Data.ThemeSupport;
using DCTTestAssignment.Data.ThemeSupport.ThemeData;
using DCTTestAssignment.Models.EventArgsModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace DCTTestAssignment.ViewModels;

public class HomeViewModel : Screen, IHandle<LanguageChanged>, IHandle<ThemeChanged>
{
    private readonly ICoinGeckoApiClient _coinGeckoApiClient;
    private readonly IEventAggregator _eventAggregator;
    private readonly IThemeDataProvider _themeDataProvider;
    private readonly SimpleContainer _container;
    private readonly ILocalizationDataProvider<IHomeViewLocalizationData> _localizationDataProvider;

    public HomeViewModel(ICoinGeckoApiClient coinGeckoApiClient, SimpleContainer container, IEventAggregator eventAggregator, ILocalizationDataProvider<IHomeViewLocalizationData> localizationDataProvider, IThemeDataProvider themeDataProvider)
    {
        _coinGeckoApiClient = coinGeckoApiClient;
        _container = container;
        _localizationDataProvider = localizationDataProvider;
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnUIThread(this);
        _themeDataProvider = themeDataProvider;
        LocalizationData = _localizationDataProvider.GetLocalizationData();
        ThemeData = _themeDataProvider.GetThemeData();
    }

    private readonly Stopwatch _typingStopWatch = Stopwatch.StartNew();

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

    private ObservableCollection<CoinsListItem> _coinsForSearchBar = new();
    public ObservableCollection<CoinsListItem> CoinsForSearchBar
    {
        get { return _coinsForSearchBar; }
        set { _coinsForSearchBar = value; NotifyOfPropertyChange(() => CoinsForSearchBar); }
    }

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

    private string? _searchText = "";
    public string? SearchText
    {
        get { return _searchText; }
        set { _searchText = value; NotifyOfPropertyChange(() => SearchText); }
    }

    private CoinsListItem? _selectedCoin;
    public CoinsListItem? SelectedCoin
    {
        get { return _selectedCoin; }
        set
        {
            _selectedCoin = value;
            NotifyOfPropertyChange(() => SelectedCoin);
            OpenDetailsAsync(SelectedCoin.Id);
        }
    }

    public async Task OpenDetailsAsync(string id)
    {
        var shellViewModel = _container.GetInstance<ShellViewModel>();
        try
        {
            await shellViewModel.OpenDetailsViewAsync(id).WaitAsync(TimeSpan.FromSeconds(10));
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(ex.Message, "Too many requests", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (TimeoutException ex)
        {
            MessageBox.Show(ex.Message, "Timeout", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public async Task SearchAsync()
    {
        Mouse.OverrideCursor = Cursors.Wait;
        var coinList = await _coinGeckoApiClient.GetCoinListAsync();
        var coin = coinList.FirstOrDefault(coin => 
            coin.Name?.ToLower() == SearchText.ToLower());

        if (coin is null)
        {
            coin = coinList.FirstOrDefault(coin =>
            coin.Symbol?.ToLower() == SearchText.ToLower());
        }

        if (coin is not null && coin.Id is not null)
        {
            await OpenDetailsAsync(coin.Id);
        }
        else
        {
            MessageBox.Show(LocalizationData?.NotFoundMessage, LocalizationData?.NotFound, MessageBoxButton.OK, MessageBoxImage.Information);
        }
        Mouse.OverrideCursor = Cursors.Arrow;
    }

    private bool _isComboBoxDropDownOpen;

    public bool IsComboBoxDropDownOpen
    {
        get { return _isComboBoxDropDownOpen; }
        set { _isComboBoxDropDownOpen = value; NotifyOfPropertyChange(() => IsComboBoxDropDownOpen); }
    }


    private int lastTimeTextChanged = 0;
    public async Task FilterCoinsAsync()
    {
        var current = Interlocked.Increment(ref lastTimeTextChanged);
        await Task.Delay(1000).ContinueWith(async task =>
        {
            if (current != lastTimeTextChanged || string.IsNullOrWhiteSpace(SearchText)) 
            {
                task.Dispose();
                return;
            };

            var coins = await _coinGeckoApiClient.GetCoinListAsync();
            var filteredCoins = coins.Where(coin => coin.Name.ToLower().Contains(SearchText)
            || coin.Symbol.ToLower().Contains(SearchText));
            CoinsForSearchBar = new ObservableCollection<CoinsListItem>(filteredCoins);
            IsComboBoxDropDownOpen = true;
        });
    }

    public Task HandleAsync(LanguageChanged message, CancellationToken cancellationToken)
    {
        LocalizationData = _localizationDataProvider.GetLocalizationData(message.Language);
        return Task.CompletedTask;
    }

    public Task HandleAsync(ThemeChanged message, CancellationToken cancellationToken)
    {
        ThemeData = _themeDataProvider.GetThemeData(message.ThemeName);
        return Task.CompletedTask;
    }
}
