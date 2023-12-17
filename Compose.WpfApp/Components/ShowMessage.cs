using Compose.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Compose.WpfApp.Components;

internal class ShowMessage : IComposable
{
    private readonly StackPanel _stackPanel = new();
    private readonly TextBox _textBox = new();
    private string _message = string.Empty;

    public virtual UIElement Compose()
    {
        _stackPanel.SetChildren(
            new Border().Configure(x =>
            {
                x.SetChild(_textBox.Configure(y =>
                {
                    y.Text = _message;
                    y.SetTextChanged(OnTextChanged);
                }));
            }),
            new TextBlock { Text = _message },
            new Button { Content = "按钮" }.Configure(x => x.Click += OnClick));
        return _stackPanel;
    }

    private void OnClick(object sender, RoutedEventArgs e) => MessageBox.Show(_message);

    private void OnTextChanged(string text)
    {
        _message = text;
        Compose();
    }
}