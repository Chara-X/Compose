using Compose.Services;

namespace Compose.Extensions;

public static class NavigationServiceExtensions
{
    public static void Push<T>(this NavigationService navigationService, params object[] parameters) => navigationService.Push(new NavigationRequest(typeof(T), parameters));
}