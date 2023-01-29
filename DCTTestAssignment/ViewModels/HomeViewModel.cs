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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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

    public async Task Search()
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
