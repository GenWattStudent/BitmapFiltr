using BitmapFiltr.ViewModels;
using Filters.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BitmapFiltr;

public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<Random>();
        services.AddInfrastructure();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<MainWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }
}
