using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using ReinstallSys.Service.Data;
using ReinstallSys.ViewModel.Controls;
using System;
using System.Windows;

namespace ReinstallSys.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            Ioc.Default.ConfigureServices(new ServiceCollection()
                .AddSingleton<DataService>()
                .AddTransient<StepBarViewModel>()
                .BuildServiceProvider());


        }

        public static ViewModelLocator Instance = new Lazy<ViewModelLocator>(() => 
        Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public static StepBarViewModel StepBar => Ioc.Default.GetRequiredService<StepBarViewModel>();
    }
}
