using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using DCTTestAssignment.Data.LocalizationData;
using DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;
using DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;
using DCTTestAssignment.Data.ThemeSupport;
using DCTTestAssignment.Data.ThemeSupport.ThemeData;
using DCTTestAssignment.Models.EventArgsModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DCTTestAssignment.ViewModels;

public class DetailsViewModel : Screen, IHandle<LanguageChanged>, IHandle<ThemeChanged>
{
    const string GreenColor = "#16C784";
    const string RedColor = "#EA3943";

    private readonly ICoinGeckoApiClient _coinGeckoApiClient;
    private readonly IThemeDataProvider _themeDataProvider;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILocalizationDataProvider<IDetailsViewLocalizationData> _localizationDataProvider;

    public DetailsViewModel(ICoinGeckoApiClient coinGeckoApiClient, ILocalizationDataProvider<IDetailsViewLocalizationData> localizationDataProvider, IEventAggregator eventAggregator, IThemeDataProvider themeDataProvider)
    {
        _coinGeckoApiClient = coinGeckoApiClient;
        _localizationDataProvider = localizationDataProvider;
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnPublishedThread(this);
        _themeDataProvider = themeDataProvider;
        LocalizationData = _localizationDataProvider.GetLocalizationData();
        ThemeData = _themeDataProvider.GetThemeData();
    }

    public async Task LoadDataAsync()
    {
        if (string.IsNullOrEmpty(CryptocurrencyId))
        {
            throw new Exception($"{nameof(CryptocurrencyId)} must not be null nor an empty string");
        }

        CoinFullData = await _coinGeckoApiClient.GetCoinFullDataAsync(CryptocurrencyId);
        if (CoinFullData.Tickets is null || CoinFullData.Tickets.Count() == 0)
        {
            DataGridVisibility = "Hidden";
            ErrorMessageVisilibity = "Visible";
        }
        else
        {
            DataGridVisibility = "Visible";
            ErrorMessageVisilibity = "Hidden";
        }
        SetColors();
    }

    private void SetColors()
    {
        if (CoinFullData is null) return;

        if (CoinFullData.MarketData.PriceChangePercentage1HInCurrency["usd"] >= 0)
        {
            Color1h = GreenColor;
        }
        else
        {
            Color1h = RedColor;
        }
        if (CoinFullData.MarketData.PriceChangePercentage24HInCurrency["usd"] >= 0)
        {
            Color24h = GreenColor;
        }
        else
        {
            Color24h = RedColor;
        }
        if (CoinFullData.MarketData.PriceChangePercentage7DInCurrency["usd"] >= 0)
        {
            Color7d = GreenColor;
        }
        else
        {
            Color7d = RedColor;
        }
    }

    private IThemeData? _themeData;
    public IThemeData? ThemeData
    {
        get { return _themeData; }
        set { _themeData = value; NotifyOfPropertyChange(() => ThemeData); }
    }

    private IDetailsViewLocalizationData? _localizationData;
    public IDetailsViewLocalizationData? LocalizationData
    {
        get { return _localizationData; }
        set { _localizationData = value; NotifyOfPropertyChange(() => LocalizationData); }
    }

    private string? _color1h;
    public string? Color1h
    {
        get { return _color1h; }
        set { _color1h = value; NotifyOfPropertyChange(() => Color1h); }
    }

    private string? _color24h;
    public string? Color24h
    {
        get { return _color24h; }
        set { _color24h = value; NotifyOfPropertyChange(() => Color24h); }
    }

    private string? _color7d;
    public string? Color7d
    {
        get { return _color7d; }
        set { _color7d = value; NotifyOfPropertyChange(() => Color7d); }
    }

    private string? _dataGridVisibility;
    public string? DataGridVisibility
    {
        get { return _dataGridVisibility; }
        set { _dataGridVisibility = value; NotifyOfPropertyChange(() => DataGridVisibility); }
    }

    private string? _errorMessageVisilibity;
    public string? ErrorMessageVisilibity
    {
        get { return _errorMessageVisilibity; }
        set { _errorMessageVisilibity = value; NotifyOfPropertyChange(() => ErrorMessageVisilibity); }
    }

    private CoinFullData? _coinFullData;
    public CoinFullData? CoinFullData
    {
        get { return _coinFullData; }
        set { _coinFullData = value; NotifyOfPropertyChange(() => CoinFullData); }
    }

    public string? CryptocurrencyId { get; set; }

    public void OpenTradeUrl(string url)
    {
        var processStartInfo = new ProcessStartInfo(url)
        {
            UseShellExecute = true,
        };
        Process.Start(processStartInfo);
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
