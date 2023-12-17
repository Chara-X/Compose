using System.Windows;
using System.Windows.Controls;

namespace Compose.Extensions;

public static class BorderExtensions
{
    public static Border SetChild(this Border border, UIElement child)
    {
        if (child is FrameworkElement { Parent: not null } element) element.RemoveParent();
        border.Child = child;
        return border;
    }
}