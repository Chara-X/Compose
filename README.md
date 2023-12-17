# Compose

Compose是用于构建原生WPF界面的工具包。它使用C#代码和直观的Compose API，可以帮助简化并加快WPF界面开发。

利用Compose，将使用声明性的方法构建一个个的界面组件，无需使用任何XML布局，也不需要使用布局编辑器、标记扩展。相反，只需调用可组合方法来定义所需的元素，Compose即会完成后面的所有工作。

## Composable

组件是可复用的UI元素，Compose围绕可组合方法构建组件。通过这些方法，可以通过描述应用的外观和提供数据依赖关系，以编程方式定义应用的UI。要创建可组合方法，只需创建一个继承`<span>IComposable</span>`的对象实现`<span>Compose</span>`方法即可，每当调用`<span class="color_font"><span>Compose</span></span>`方法都会触发组件的重新渲染，如果需要组件参数则对其进行overload，如果需要被继承则对其加virtual修饰进行override。组件的根元素、子组件以及需要确保焦点不因渲染而丢失的元素如`<span class="color_font"><span>TextBox</span></span>`，需要定义在类作用域并保证只读，其它元素都可以定义在方法里，可以依靠`<span class="color_font"><span>Configure</span></span>`扩展方法来更方便的配置元素。

在WPF中，某些元素在分配子元素时不会清除其与前一个父级元素的连接，强行赋值会导致报错，例如`<span>Panel</span>`的`<span>Children</span>`属性、`<span>Border</span>`的`<span>Child</span>`属性，需要借助一些扩展方法如`<span>SetChildren</span>`、`<span>SetChild</span>`来进行赋值，它们会在添加子元素前清除与其前一个父级元素的连接。

下面的例子中用Compose实现了组件的许多特性，如嵌套组件、组件参数、数据绑定、事件绑定、双向绑定、条件渲染、级联样式等等：

```
internal class MainPage : IComposable
{
    // 级联样式
    private readonly StackPanel _stackPanel = new() { Resources = new ResourceDictionary { Source = new Uri("Resources/Styles.xaml", UriKind.Relative) } };

    private readonly Counter _counter = new();
    private int _value;

    public UIElement Compose()
    {
        _stackPanel.SetChildren(new StackPanel().Configure(x =>
        {
            x.SetChildren(
                new TextBlock { Text = "Hello, world!", Style = (Style)_stackPanel.Resources["Heading"]! },
                new TextBlock { Text = "Welcome to your new app." },
                // 嵌套组件
                _counter.Compose(_value, OnCounterValueChanged));
        }));
        // 条件渲染
        for (var i = 0; i < _value; i++) _stackPanel.Children.Add(new TextBlock { Text = "Item：" + i });
        return _stackPanel;
    }

    private void OnCounterValueChanged(int value)
    {
        _value = value;
        Compose();
    }
}
```

```
internal class Counter : IComposable
{
    private readonly StackPanel _stackPanel = new();
    private readonly TextBox _textBox = new();
    private int _value;
    private Action<int> _valueChanged = null!;

    public UIElement Compose(int value, Action<int> valueChanged)
    {
        (_value, _valueChanged) = (value, valueChanged);
        return Compose();
    }

    public UIElement Compose()
    {
        _stackPanel.SetChildren(
            // 数据绑定
            new TextBlock { Text = "Count：" + _value },
            // 事件绑定
            new Button { Content = "Add" }.Configure(x => x.Click += (_, _) => _valueChanged(++_value)),
            // 双向绑定
            _textBox.Configure(x =>
            {
                x.Text = _value.ToString();
                x.SetTextChanged(y =>
                {
                    if (int.TryParse(y, out var value)) _valueChanged(value);
                });
            }));
        return _stackPanel;
    }
}
```

```
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml">
    <Style TargetType="StackPanel">
        <Setter Property="Background" Value="SlateBlue"/>
    </Style>
    <Style TargetType="TextBlock">
        <Setter Property="FontSize" Value="20"/>
    </Style>
    <Style x:Key="Heading" TargetType="TextBlock" BasedOn="{StaticResource {x:Type TextBlock}}">
        <Setter Property="FontSize" Value="30"/>
    </Style>
</ResourceDictionary>
```

![](http://www.kdocs.cn/api/v3/office/copy/L0FFOE1DQUprZ01Pb3p4VWZobEZ6TEU3MVlCb0xFV0llZzlodzdSeVRpc01YUXpsWGJPenEwbVV3bHg3UGxqVXdRL1F2K1dpSWtOcDFReDRaYlhSdmZCQzhhd2NnY2FvUU9HTUxuL1V6L3VnUEx6UjRhalpTWGpFRW1tWTJ6WEFzOEVSK25GSU4wOWt4UCtkWHVuc0ZSSW5YK2dLY3ZDemVJQ3NERmRjZmRidkxkV2dKOURSMWpITi9QZzlFc2JtVmVpRHBwc1M4M3dTVHpVaGFtaE1UNWJpLytVRkhXS2dTcmVUSDc3Rlg3Z0xtT3VYWGh1VVdlRmlRN0lzZHdkb3lYYkxSVFFrek84PQ==/attach/object/GXJIUBIAYE?)

## Navigation & Dependency injection

Compose中，任何对象都可以作为页面，使用`<span>NavigationService</span>`管理代码中的导航，如果对象继承了`<span>IComposable</span>`接口，则采用其`<span>Compose</span>`方法生成的组件来作为页面。

某些页面可能需要依赖服务或者路由参数，需要采用构造方法进行注入，通过向构造方法添加参数来添加所需的服务和路由参数。当导航到该页面时，会相应地提供这些实例。在下面的示例中，构造函数通过DI接收`<span>NavigationService</span>`进行导航，并通过一个`<span>message</span>`路由参数来传递消息。

```
internal class BeforePage(NavigationService navigationService, string message) : IComposable
{
    private readonly StackPanel _stackPanel = new();

    public UIElement Compose()
    {
        _stackPanel.SetChildren(
            new TextBlock { Text = message },
            new Button { Content = "Push" }.Configure(x => x.Click += OnClick));
        return _stackPanel;
    }

    private void OnClick(object sender, RoutedEventArgs e) => navigationService.Push<AfterPage>("Hello from BeforePage");
}
```

```
internal class AfterPage(NavigationService navigationService, string message) : IComposable
{
    private readonly StackPanel _stackPanel = new();

    public UIElement Compose()
    {
        _stackPanel.SetChildren(
            new TextBlock { Text = message },
            new Button { Content = "Pop" }.Configure(x => x.Click += OnClick));
        return _stackPanel;
    }

    private void OnClick(object sender, RoutedEventArgs e) => navigationService.Pop();
}
```

```
public partial class MainWindow
{
    public MainWindow(NavigationService navigationService)
    {
        InitializeComponent();
        //Content = new MainPage().Compose();
        Loaded += (_, _) => navigationService.Push<BeforePage>("Hello from MainWindow");
    }
}
```

## Links

* [Azure Repos](1)
