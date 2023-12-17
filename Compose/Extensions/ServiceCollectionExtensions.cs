using Compose.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Compose.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddNavigation(this IServiceCollection services) => services.AddSingleton(x => new NavigationService(x));
}