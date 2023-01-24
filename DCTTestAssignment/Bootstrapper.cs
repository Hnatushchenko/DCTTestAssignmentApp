using Caliburn.Micro;
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
    public Bootstrapper()
    { 
        Initialize();
    }

    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellViewModel>();
        base.OnStartup(sender, e);
    }
}
