using Caliburn.Micro;
using CoinGecko;
using CoinGecko.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCTTestAssignment.ViewModels;

public class ShellViewModel : Conductor<object>
{
    private readonly SimpleContainer _container;

    public ShellViewModel(SimpleContainer container)
	{
        _container = container;
        ActivateItemAsync(_container.GetInstance<HomeViewModel>()).Wait();
    }

    public async Task LoadHomePage()
    {
        if (ActiveItem.GetType() == typeof(HomeViewModel))
            return;

        await ActivateItemAsync(_container.GetInstance<HomeViewModel>());
    }
}
