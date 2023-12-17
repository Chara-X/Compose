using System.Reflection;
using System.Windows;
// ReSharper disable CoVariantArrayConversion

namespace Compose.Extensions;

internal static class UiElementExtensions
{
    public static void RemoveHandler<T>(this T element, RoutedEvent routedEvent) where T : UIElement
    {
        var eventHandlersStore = typeof(UIElement).GetProperty("EventHandlersStore", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!.GetValue(element)!;
        var eventHandlerInfos = (RoutedEventHandlerInfo[]?)eventHandlersStore.GetType().GetMethod("GetRoutedEventHandlers")!.Invoke(eventHandlersStore, new[] { routedEvent });
        if (eventHandlerInfos == null) return;
        foreach (var x in eventHandlerInfos.Select(x => x.Handler)) element.RemoveHandler(routedEvent, x);
    }
}