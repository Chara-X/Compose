using System.Windows;

namespace Compose.Extensions;

public static class DependencyObjectExtensions
{
    public static T Configure<T>(this T dependencyObject, Action<T> configure) where T : DependencyObject
    {
        configure(dependencyObject);
        return dependencyObject;
    }
}