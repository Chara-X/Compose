using System.Collections;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Compose.Services;

public class NavigationService(IServiceProvider provider) : IEnumerable<NavigationRequest>
{
    private readonly Stack<NavigationRequest> _stack = new();
    public event Action? Navigated;

    public void Push(NavigationRequest request)
    {
        if (Application.Current.MainWindow!.Content is IDisposable disposable)
            disposable.Dispose();
        _stack.Push(request);
        Application.Current.MainWindow!.Content = CreatePage(_stack.Peek());
        Navigated?.Invoke();
    }

    public void Pop()
    {
        _stack.Pop();
        Application.Current.MainWindow!.Content = CreatePage(_stack.Peek());
        Navigated?.Invoke();
    }

    private object CreatePage(NavigationRequest request) => ActivatorUtilities.CreateInstance(provider, request.Type, request.Parameters) switch { IComposable composable => composable.Compose(), { } element => element, _ => throw new InvalidOperationException() };
    public IEnumerator<NavigationRequest> GetEnumerator() => _stack.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public record NavigationRequest(Type Type, object[] Parameters);