using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Compose.Extensions;

public static class TextBoxExtensions
{
    public static void SetTextChanged(this TextBox textBox, Action<string> textChanged)
    {
        textBox.RemoveHandler(TextBoxBase.TextChangedEvent);
        textBox.TextChanged += (_, _) => textChanged(textBox.Text);
    }
}