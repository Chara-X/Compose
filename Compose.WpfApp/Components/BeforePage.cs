using System.Windows.Controls;
using System.Windows;
using Compose.Extensions;
using Compose.Services;

namespace Compose.WpfApp.Components;

internal class BeforePage(NavigationService navigationService, string message) : IComposable
{
    private readonly StackPanel _stackPanel = new();

    public UIElement Compose()
    {
        _stackPanel.SetChildren(
            new TextBlock { Text = message },
            new Button { Content = "Push" }.Configure(x => x.Click += OnClick));
        return _stackPanel;
    }

    private void OnClick(object sender, RoutedEventArgs e) => navigationService.Push<AfterPage>("Hello from BeforePage");
}