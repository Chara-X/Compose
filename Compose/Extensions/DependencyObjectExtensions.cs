using System.Windows;

namespace Compose.Extensions;

/// <summary>
/// 更新界面
/// </summary>
public static class DependencyObjectExtensions
{
    /// <summary>
    /// 更新界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dependencyObject">图形界面</param>
    /// <param name="configure">操作{图形界面}</param>
    /// <returns></returns>
    public static T Configure<T>(this T dependencyObject, Action<T> configure) where T : DependencyObject
    {
        configure(dependencyObject);
        return dependencyObject;
    }
}