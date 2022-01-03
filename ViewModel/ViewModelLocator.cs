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
                .AddTransient<BeforDeploymentViewModel>()
                .AddTransient<SoftwareViewModel>()
                .AddTransient<PrinterViewModel>()
                .AddTransient<PrinterIPViewModel>()
                .AddTransient<OfficeInstallViewModel>()
                .AddTransient<OfficeUninstallViewModel>()
                .BuildServiceProvider());


        }

        public static ViewModelLocator Instance = new Lazy<ViewModelLocator>(() => 
        Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public static StepBarViewModel StepBar => Ioc.Default.GetRequiredService<StepBarViewModel>();
        public static BeforDeploymentViewModel BeforDeployment => Ioc.Default.GetRequiredService<BeforDeploymentViewModel>();
        public static SoftwareViewModel Software => Ioc.Default.GetRequiredService<SoftwareViewModel>();
        public static PrinterViewModel Printer => Ioc.Default.GetRequiredService<PrinterViewModel>();
        public static PrinterIPViewModel PrinterIP => Ioc.Default.GetRequiredService<PrinterIPViewModel>();
        public static OfficeInstallViewModel OfficeInstall => Ioc.Default.GetRequiredService<OfficeInstallViewModel>();
        public static OfficeUninstallViewModel OfficeUninstall => Ioc.Default.GetRequiredService<OfficeUninstallViewModel>();
    }
}
