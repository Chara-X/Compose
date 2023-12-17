using System.Windows.Controls;
using System.Windows;
using Compose.Extensions;
using Compose.Services;

namespace Compose.WpfApp.Components;

internal class AfterPage(NavigationService navigationService, string message) : IComposable
{
    private readonly StackPanel _stackPanel = new();

    public UIElement Compose()
    {
        _stackPanel.SetChildren(
            new TextBlock { Text = message },
            new Button { Content = "Pop" }.Configure(x => x.Click += OnClick));
        return _stackPanel;
    }

    private void OnClick(object sender, RoutedEventArgs e) => navigationService.Pop();
}