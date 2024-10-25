using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace WpfExtensions.Controls;

public class TextBoxEx : TextBox
{
    private bool _isFocusedFirstTime;
    private string _lastText = string.Empty;
    private bool _isTextChanged;

    static TextBoxEx()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxEx), new FrameworkPropertyMetadata(typeof(TextBoxEx)));
    }

    #region CornerRadius

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public static readonly DependencyProperty CornerRadiusProperty =
        DependencyProperty.Register(nameof(CornerRadius), typeof(CornerRadius), typeof(TextBoxEx), new PropertyMetadata(default(CornerRadius)));

    #endregion

    #region Watermark

    public string Watermark
    {
        get => (string)GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }

    public static readonly DependencyProperty WatermarkProperty =
        DependencyProperty.Register(nameof(Watermark), typeof(string), typeof(TextBoxEx), new PropertyMetadata(string.Empty));

    #endregion

    #region WatermarkOpacity

    public double WatermarkOpacity
    {
        get => (double)GetValue(WatermarkOpacityProperty);
        set => SetValue(WatermarkOpacityProperty, value);
    }

    public static readonly DependencyProperty WatermarkOpacityProperty =
        DependencyProperty.Register(nameof(WatermarkOpacity), typeof(double), typeof(TextBoxEx), new PropertyMetadata(1d));

    #endregion

    #region Icon

    public object Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register(nameof(Icon), typeof(object), typeof(TextBoxEx), new PropertyMetadata(default(object)));

    #endregion

    #region IconMargin

    public Thickness IconMargin
    {
        get => (Thickness)GetValue(IconMarginProperty);
        set => SetValue(IconMarginProperty, value);
    }

    public static readonly DependencyProperty IconMarginProperty =
        DependencyProperty.Register(nameof(IconMargin), typeof(Thickness), typeof(TextBoxEx), new PropertyMetadata(default(Thickness)));

    #endregion

    #region SelectAllTextOnFocus

    public bool SelectAllTextOnFocus
    {
        get => (bool)GetValue(SelectAllTextOnFocusProperty);
        set => SetValue(SelectAllTextOnFocusProperty, value);
    }

    public static readonly DependencyProperty SelectAllTextOnFocusProperty =
        DependencyProperty.Register(nameof(SelectAllTextOnFocus), typeof(bool), typeof(TextBoxEx), new PropertyMetadata(default(bool)));

    #endregion

    #region IsConfirmRejectEnabled

    public bool IsConfirmRejectEnabled
    {
        get => (bool)GetValue(IsConfirmRejectEnabledProperty);
        set => SetValue(IsConfirmRejectEnabledProperty, value);
    }

    public static readonly DependencyProperty IsConfirmRejectEnabledProperty =
        DependencyProperty.Register(nameof(IsConfirmRejectEnabled), typeof(bool), typeof(TextBoxEx), new PropertyMetadata(true));

    #endregion

    #region ConfirmKey

    public Key ConfirmKey
    {
        get => (Key)GetValue(ConfirmKeyProperty);
        set => SetValue(ConfirmKeyProperty, value);
    }

    public static readonly DependencyProperty ConfirmKeyProperty =
        DependencyProperty.Register(nameof(ConfirmKey), typeof(Key), typeof(TextBoxEx), new PropertyMetadata(Key.Enter));

    #endregion

    #region RejectKey

    public Key RejectKey
    {
        get => (Key)GetValue(RejectKeyProperty);
        set => SetValue(RejectKeyProperty, value);
    }

    public static readonly DependencyProperty RejectKeyProperty =
        DependencyProperty.Register(nameof(RejectKey), typeof(Key), typeof(TextBoxEx), new PropertyMetadata(Key.Escape));

    #endregion

    #region ConfirmedRoutedEvent

    public static readonly RoutedEvent ConfirmedRoutedEvent =
        EventManager.RegisterRoutedEvent(nameof(Confirmed), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextBoxEx));

    public event RoutedEventHandler Confirmed
    {
        add => AddHandler(ConfirmedRoutedEvent, value);
        remove => RemoveHandler(ConfirmedRoutedEvent, value);
    }

    #endregion

    #region RejectedRoutedEvent

    public static readonly RoutedEvent RejectedRoutedEvent =
        EventManager.RegisterRoutedEvent(nameof(Rejected), RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TextBoxEx));

    public event RoutedEventHandler Rejected
    {
        add => AddHandler(RejectedRoutedEvent, value);
        remove => RemoveHandler(RejectedRoutedEvent, value);
    }

    #endregion

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        SelectionChanged += OnSelectionChanged;
        TextChanged += OnTextChanged;
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (!IsConfirmRejectEnabled)
            return;

        if (e.Key == ConfirmKey)
        {
            BindingOperations.GetBindingExpression(this, TextProperty)?.UpdateSource();
            OnConfirm();
            _lastText = Text;
            SelectAllText();
        }
        else if (e.Key == RejectKey)
        {
            Text = _lastText;
            _isTextChanged = false;
            SelectAllText();
            OnReject();
        }
    }

    protected virtual void OnConfirm() => RaiseEvent(new RoutedEventArgs(ConfirmedRoutedEvent, this));

    protected virtual void OnReject() => RaiseEvent(new RoutedEventArgs(RejectedRoutedEvent, this));

    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);

        _isTextChanged = false;
        _lastText = Text;
        _isFocusedFirstTime = true;
    }

    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);

        if (_isTextChanged)
        {
            BindingOperations.GetBindingExpression(this, TextProperty)?.UpdateSource();
            OnConfirm();
        }
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        _isTextChanged = true;
    }

    private void OnSelectionChanged(object sender, RoutedEventArgs e)
    {
        if (!SelectAllTextOnFocus) return;
        if (!_isFocusedFirstTime) return;

        _isFocusedFirstTime = false;
        SelectAll();
    }

    private void SelectAllText() => Dispatcher.Invoke(SelectAll);
}