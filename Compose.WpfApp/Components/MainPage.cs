using Compose.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Compose.WpfApp.Components;

internal class MainPage : IComposable
{
    // Cascade Style
    private readonly StackPanel _stackPanel = new() { Resources = new ResourceDictionary { Source = new Uri("Resources/Styles.xaml", UriKind.Relative) } };

    private readonly Counter _counter = new();
    private int _value;

    public UIElement Compose()
    {
        _stackPanel.SetChildren(new StackPanel().Configure(x =>
        {
            x.SetChildren(
                new TextBlock { Text = "Hello, world!", Style = (Style)_stackPanel.Resources["Heading"]! },
                new TextBlock { Text = "Welcome to your new app." },
                // Child component
                _counter.Compose(_value, OnCounterValueChanged));
        }));
        // Condition render
        for (var i = 0; i < _value; i++) _stackPanel.Children.Add(new TextBlock { Text = "Item：" + i });
        return _stackPanel;
    }

    private void OnCounterValueChanged(int value)
    {
        _value = value;
        Compose();
    }
}
