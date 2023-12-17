using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Compose.Extensions;

public static class PanelExtensions
{
    public static void SetBackground(this Panel panel, Color color) => panel.Background = new SolidColorBrush(color);

    public static void SetChildren(this Panel panel, params UIElement[] children)
    {
        panel.Children.Clear();
        foreach (var x in children)
        {
            if (x is FrameworkElement { Parent: not null } element) element.RemoveParent();
            panel.Children.Add(x);
        }
    }
}