using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using ReinstallSys.Service.Data;
using ReinstallSys.ViewModel.Controls;
using ReinstallSys.ViewModel.Controls.PrinterViewModel;
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
                .AddTransient<PrinterEasyModelViewModel>()
                .AddTransient<PrinterAdvanceModelViewModel>()
                .AddTransient<PrinterCustomModelViewModel>()
                .AddTransient<OfficeInstallViewModel>()
                .AddTransient<PrinterRootViewModel>()
                .AddTransient<ActivatorViewModel>()
                .BuildServiceProvider());


        }

        public static ViewModelLocator Instance = new Lazy<ViewModelLocator>(() => 
        Application.Current.TryFindResource("Locator") as ViewModelLocator).Value;

        public static StepBarViewModel StepBar => Ioc.Default.GetRequiredService<StepBarViewModel>();
        public static BeforDeploymentViewModel BeforDeployment => Ioc.Default.GetRequiredService<BeforDeploymentViewModel>();
        public static SoftwareViewModel Software => Ioc.Default.GetRequiredService<SoftwareViewModel>();
        public static PrinterEasyModelViewModel PrinterEasyModel => Ioc.Default.GetRequiredService<PrinterEasyModelViewModel>();
        public static PrinterAdvanceModelViewModel PrinterAdvanceModel => Ioc.Default.GetRequiredService<PrinterAdvanceModelViewModel>();
        public static PrinterCustomModelViewModel PrinterCustomModel => Ioc.Default.GetRequiredService<PrinterCustomModelViewModel>();
        public static PrinterRootViewModel PrinterRoot => Ioc.Default.GetRequiredService<PrinterRootViewModel>();
        public static OfficeInstallViewModel OfficeInstall => Ioc.Default.GetRequiredService<OfficeInstallViewModel>();        
        public static ActivatorViewModel Activator => Ioc.Default.GetRequiredService<ActivatorViewModel>();
    }
}
