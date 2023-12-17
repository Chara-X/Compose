using Compose.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace Compose.WpfApp.Components;

internal class Counter : IComposable
{
    private readonly StackPanel _stackPanel = new();
    private readonly TextBox _textBox = new();
    private int _value;
    private Action<int> _valueChanged = null!;

    public UIElement Compose(int value, Action<int> valueChanged)
    {
        (_value, _valueChanged) = (value, valueChanged);
        return Compose();
    }

    public UIElement Compose()
    {
        _stackPanel.SetChildren(
            // 数据绑定
            new TextBlock { Text = "Count：" + _value },
            // 事件绑定
            new Button { Content = "Add" }.Configure(x => x.Click += (_, _) => _valueChanged(++_value)),
            // 双向绑定
            _textBox.Configure(x =>
            {
                x.Text = _value.ToString();
                x.SetTextChanged(y =>
                {
                    if (int.TryParse(y, out var value)) _valueChanged(value);
                });
            }));
        return _stackPanel;
    }
}