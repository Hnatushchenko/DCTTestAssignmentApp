using Caliburn.Micro;
using CoinGecko;
using DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;
using DCTTestAssignment.Data.ThemeSupport.ThemeData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCTTestAssignment.Data.LocalizationData.ConvertViewLocalizationData;
using DCTTestAssignment.Data.LocalizationData;
using DCTTestAssignment.Models.EventArgsModels;
using System.Threading;

namespace DCTTestAssignment.ViewModels;

public class ConvertViewModel : Screen, IHandle<LanguageChanged>
{
	private readonly ICoinGeckoApiClient _coinGeckoApiClient;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILocalizationDataProvider<IConvertViewLocalizationData> _localizationDataProvider;
    public ConvertViewModel(ICoinGeckoApiClient coinGeckoApiClient, ILocalizationDataProvider<IConvertViewLocalizationData> localizationDataProvider, IEventAggregator eventAggregator)
    {
        _coinGeckoApiClient = coinGeckoApiClient;
        _localizationDataProvider = localizationDataProvider;
        _eventAggregator = eventAggregator;
        _eventAggregator.SubscribeOnPublishedThread(this);
    }

    private IThemeData? _themeData;
    public IThemeData? ThemeData
    {
        get { return _themeData; }
        set { _themeData = value; NotifyOfPropertyChange(() => ThemeData); }
    }

    private IConvertViewLocalizationData? _localizationData;
    public IConvertViewLocalizationData? LocalizationData
    {
        get { return _localizationData; }
        set { _localizationData = value; NotifyOfPropertyChange(() => LocalizationData); }
    }

    private string? _currencyNameToConvertFrom;
	public string? CurrencyNameToConvertFrom
    {
		get { return _currencyNameToConvertFrom; }
		set { _currencyNameToConvertFrom = value; NotifyOfPropertyChange(() => CurrencyNameToConvertFrom); }
	}

	private string? _currencyNameToConvertTo;
	public string? CurrencyNameToConvertTo
    {
		get { return _currencyNameToConvertTo; }
		set { _currencyNameToConvertTo = value; NotifyOfPropertyChange(() => CurrencyNameToConvertTo); }
	}

    private string? _currencyAmountToConvertFrom;
    public string? CurrencyAmountToConvertFrom
    {
        get { return _currencyAmountToConvertFrom; }
        set { _currencyAmountToConvertFrom = value; NotifyOfPropertyChange(() => CurrencyAmountToConvertFrom); }
    }

    private string? _calculatedAmount;
    public string? CalculatedAmount
    {
        get { return _calculatedAmount; }
        set { _calculatedAmount = value; NotifyOfPropertyChange(() => CalculatedAmount); }
    }

    public async Task ConvertAsync()
	{
        CalculatedAmount = "";
		if (string.IsNullOrWhiteSpace(CurrencyNameToConvertFrom)) return;
        if (string.IsNullOrWhiteSpace(CurrencyNameToConvertTo)) return;
        if (decimal.TryParse(CurrencyAmountToConvertFrom, out decimal amountToConvertFrom) == false) return;

        CalculatedAmount = LocalizationData?.Loading;

        int timeout = 10000;
        var coinToConvertFromPriceTask = _coinGeckoApiClient.GetCoinPriceInUSDByNameOrSymbol(CurrencyNameToConvertFrom);
        if (await Task.WhenAny(coinToConvertFromPriceTask, Task.Delay(timeout)) != coinToConvertFromPriceTask)
        {
            CalculatedAmount = LocalizationData?.TimeoutMessage;
            return;
        }

        var coinToConvertToPriceTask = _coinGeckoApiClient.GetCoinPriceInUSDByNameOrSymbol(CurrencyNameToConvertTo);
        if (await Task.WhenAny(coinToConvertToPriceTask, Task.Delay(timeout)) != coinToConvertToPriceTask)
        {
            CalculatedAmount = LocalizationData?.TimeoutMessage;
            return;
        }

        decimal? coinToConvertFromPrice = coinToConvertFromPriceTask.Result;
        decimal? coinToConvertToPrice = coinToConvertToPriceTask.Result;

        if (coinToConvertFromPrice is null)
        {
            CalculatedAmount = LocalizationData?.CoinNotFoundMessage;
            return;
        }
        if (coinToConvertToPrice is null)
        {
            CalculatedAmount = LocalizationData?.CoinNotFoundMessage;
            return;
        }

        CalculatedAmount = (coinToConvertFromPrice * amountToConvertFrom / coinToConvertToPrice).ToString();
    }

    public Task HandleAsync(LanguageChanged message, CancellationToken cancellationToken)
    {
        LocalizationData = _localizationDataProvider.GetLocalizationData(message.Language);
        return Task.CompletedTask;
    }
}
