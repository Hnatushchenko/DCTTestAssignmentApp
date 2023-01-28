using Caliburn.Micro;
using CoinGecko;
using DCTTestAssignment.Data;
using DCTTestAssignment.Data.LocalizationData.ConvertViewLocalizationData;
using DCTTestAssignment.Data.LocalizationData.DetailsViewLocalizationData;
using DCTTestAssignment.Data.LocalizationData.HomeLocalizationData;
using DCTTestAssignment.Data.LocalizationData.ShellViewLocalizationData;
using DCTTestAssignment.Data.ThemeSupport;
using DCTTestAssignment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DCTTestAssignment;

public class Bootstrapper : BootstrapperBase
{
    private readonly SimpleContainer _container;

    public Bootstrapper()
    { 
        _container = new SimpleContainer();
        Initialize();
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellViewModel>();
    }

    protected override void Configure()
    {
        _container.Instance(_container);
        _container.Singleton<IWindowManager, WindowManager>();
        _container.Singleton<IEventAggregator, EventAggregator>();
        _container.Singleton<ICoinGeckoApiClient, CoinGeckoApiClient>();
        _container.Singleton<IThemeDataProvider, ThemeDataProvider>();
        _container.Singleton<ILocalizationDataProvider<IShellViewLocalizationData>, LocalizationDataProvider<IShellViewLocalizationData>>();
        _container.Singleton<ILocalizationDataProvider<IHomeViewLocalizationData>, LocalizationDataProvider<IHomeViewLocalizationData>>();
        _container.Singleton<ILocalizationDataProvider<IDetailsViewLocalizationData>, LocalizationDataProvider<IDetailsViewLocalizationData>>();
        _container.Singleton<ILocalizationDataProvider<IConvertViewLocalizationData>, LocalizationDataProvider<IConvertViewLocalizationData>>();
        _container.Singleton<ShellViewModel>();
        _container.Singleton<DetailsViewModel>();
        _container.Singleton<HomeViewModel>();
        _container.Singleton<ConvertViewModel>();
    }

    protected override object GetInstance(Type service, string key)
    {
        return _container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance)
    {
        _container.BuildUp(instance);
    }
}
