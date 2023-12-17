using System.Windows;

namespace Compose;

public interface IComposable
{
    UIElement Compose();
}