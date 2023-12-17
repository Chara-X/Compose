using System.Windows;
using Compose.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Compose.WpfApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        services.AddNavigation();
        services.AddSingleton<MainWindow>();
        var serviceProvider = services.BuildServiceProvider();
        var window = serviceProvider.GetRequiredService<MainWindow>();
        window.Show();
    }
}