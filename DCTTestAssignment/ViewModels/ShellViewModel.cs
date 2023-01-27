using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using DCTTestAssignment.Data;
using DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;
using DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;
using DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DCTTestAssignment.ViewModels;

public class ShellViewModel : Conductor<object>
{
    private readonly HomeViewModel _homeViewModel;
    private readonly DetailsViewModel _detailsViewModel;
    private readonly ILocalizationDataProvider<IShellViewLocalizationData> _localizationDataProvider;

    private IShellViewLocalizationData _localizationData = null!;
    public IShellViewLocalizationData LocalizationData
    {
        get { return _localizationData; }
        set { _localizationData = value; NotifyOfPropertyChange(() => LocalizationData); }
    }

    public ShellViewModel(ILocalizationDataProvider<IShellViewLocalizationData> localizationDataProvider, HomeViewModel homeViewModel, DetailsViewModel detailsViewModel)
    {
        _localizationDataProvider = localizationDataProvider;
        _homeViewModel = homeViewModel;
        _detailsViewModel = detailsViewModel;
        SelectedLanguage = "English";
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

    private void ChangeApplicationLanguage()
    {
        LocalizationData = _localizationDataProvider.GetLocalizationData(SelectedLanguage);
        _homeViewModel.UpdateLanguage(SelectedLanguage);
        _detailsViewModel.UpdateLanguage(SelectedLanguage);
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
}
