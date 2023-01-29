using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using DCTTestAssignment.Data.LocalizationData;
using DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;
using DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;
using DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;
using DCTTestAssignment.Data.ThemeSupport;
using DCTTestAssignment.Data.ThemeSupport.ThemeData;
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
    private readonly HomeViewModel _homeViewModel;
    private readonly DetailsViewModel _detailsViewModel;
    private readonly ConvertViewModel _convertViewModel;
    private readonly IThemeDataProvider _themeDataProvider;
    private readonly ILocalizationDataProvider<IShellViewLocalizationData> _localizationDataProvider;

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

    public ShellViewModel(ILocalizationDataProvider<IShellViewLocalizationData> localizationDataProvider, HomeViewModel homeViewModel, DetailsViewModel detailsViewModel, IThemeDataProvider themeDataProvider, ConvertViewModel convertViewModel)
    {
        _localizationDataProvider = localizationDataProvider;
        _homeViewModel = homeViewModel;
        _detailsViewModel = detailsViewModel;
        _themeDataProvider = themeDataProvider;
        _convertViewModel = convertViewModel;
        SelectedLanguage = "English";
        SelectedTheme = "Light";
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
        LocalizationData = _localizationDataProvider.GetLocalizationData(SelectedLanguage);
        _homeViewModel.UpdateLanguage(SelectedLanguage);
        _detailsViewModel.UpdateLanguage(SelectedLanguage);
        _convertViewModel.UpdateLanguage(SelectedLanguage);
    }

    private void ChangeApplicationTheme()
    {
        ThemeData = _themeDataProvider.GetThemeData(SelectedTheme);
        _homeViewModel.ThemeData = ThemeData;
        _detailsViewModel.ThemeData = ThemeData;
        _convertViewModel.ThemeData = ThemeData;
    }

    protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
    {
        await LoadHomePage();
    }

    public async Task LoadHomePage()
    {
        if (ActiveItem?.GetType() == typeof(HomeViewModel))
            return;

        await ActivateItemAsync(_homeViewModel);
    }

    public async Task OpenDetailsViewAsync(string cryptocurrencyId)
    {
        _detailsViewModel.CryptocurrencyId = cryptocurrencyId;
        await _detailsViewModel.LoadDataAsync();
        await ActivateItemAsync(_detailsViewModel);
    }

    public async Task LoadConverterPageAsync()
    {
        if (ActiveItem?.GetType() == typeof(ConvertViewModel))
            return;

        await ActivateItemAsync(_convertViewModel);
    }
}
