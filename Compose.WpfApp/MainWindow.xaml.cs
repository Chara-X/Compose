using Compose.Extensions;
using Compose.Services;
using Compose.WpfApp.Components;

namespace Compose.WpfApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow(NavigationService navigationService)
    {
        InitializeComponent();
        Content = new MainPage().Compose();
        //Loaded += (_, _) => navigationService.Push<BeforePage>("Hello from MainWindow");
    }
}