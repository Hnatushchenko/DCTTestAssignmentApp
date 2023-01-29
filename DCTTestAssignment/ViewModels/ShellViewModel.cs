using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using DCTTestAssignment.Data.LocalizationData;
using DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;
using DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;
using DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;
using DCTTestAssignment.Data.ThemeSupport;
using DCTTestAssignment.Data.ThemeSupport.ThemeData;
using DCTTestAssignment.Models.EventArgsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Diagnostics;

namespace DCTTestAssignment.ViewModels;

public class ShellViewModel : Conductor<object>
{
    private readonly IThemeDataProvider _themeDataProvider;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILocalizationDataProvider<IShellViewLocalizationData> _localizationDataProvider;
    private readonly SimpleContainer _container;

    private IShellViewLocalizationData _localizationData = null!;
    public IShellViewLocalizationData LocalizationData
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

    public ShellViewModel(ILocalizationDataProvider<IShellViewLocalizationData> localizationDataProvider, IThemeDataProvider themeDataProvider, IEventAggregator eventAggregator, SimpleContainer container)
    {
        _localizationDataProvider = localizationDataProvider;
        _themeDataProvider = themeDataProvider;
        _eventAggregator = eventAggregator;
        _container = container;
        SelectedLanguage = _localizationDataProvider.CurrentLocalizationName;
        SelectedTheme = _themeDataProvider.CurrentThemeName;
    }

    private string _selectedLanguage = null!;
    public string SelectedLanguage
    {
        get { return _selectedLanguage; }
        set
        {
            _selectedLanguage = value;
            NotifyOfPropertyChange(() => SelectedLanguage);
            ChangeApplicationLanguage();
        }
    }

    private string _selectedTheme = null!;
    public string SelectedTheme
    {
        get { return _selectedTheme; }
        set
        {
            _selectedTheme = value;
            NotifyOfPropertyChange(() => SelectedTheme);
            ChangeApplicationTheme();
        }
    }

    private void ChangeApplicationLanguage()
    {
        _localizationDataProvider.CurrentLocalizationName = SelectedLanguage;
        LocalizationData = _localizationDataProvider.GetLocalizationData();
        _eventAggregator.PublishOnCurrentThreadAsync(new LanguageChanged(SelectedLanguage));
    }

    private void ChangeApplicationTheme()
    {
        _themeDataProvider.CurrentThemeName = SelectedTheme;
        ThemeData = _themeDataProvider.GetThemeData();
        _eventAggregator.PublishOnCurrentThreadAsync(new ThemeChanged(SelectedTheme));
    }

    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        await LoadHomePageAsync();
    }

    public async Task LoadHomePageAsync()
    {
        if (ActiveItem?.GetType() == typeof(HomeViewModel))
            return;

        var homeViewModel = _container.GetInstance<HomeViewModel>();
        await ActivateItemAsync(homeViewModel);
    }

    public async Task OpenDetailsViewAsync(string cryptocurrencyId)
    {
        var detailsViewModel = _container.GetInstance<DetailsViewModel>();
        detailsViewModel.CryptocurrencyId = cryptocurrencyId;
        await detailsViewModel.LoadDataAsync();
        await ActivateItemAsync(detailsViewModel);
    }

    public async Task LoadConverterPageAsync()
    {
        if (ActiveItem?.GetType() == typeof(ConvertViewModel))
            return;

        var convertViewModel = _container.GetInstance<ConvertViewModel>();
        await ActivateItemAsync(convertViewModel);
    }
}
