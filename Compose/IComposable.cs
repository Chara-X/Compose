using System.Windows;

namespace Compose;

/// <summary>
/// 图形组件
/// </summary>
public interface IComposable
{
    /// <summary>
    /// 更新界面
    /// </summary>
    /// <returns>图形界面</returns>
    UIElement Compose();
}