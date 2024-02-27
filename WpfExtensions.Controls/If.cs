using System.Windows;
using System.Windows.Controls;

namespace WpfExtensions.Controls;

public class If : ContentControl
{
    #region True

    public object True
    {
        get => GetValue(TrueProperty);
        set => SetValue(TrueProperty, value);
    }

    public static readonly DependencyProperty TrueProperty =
        DependencyProperty.Register(nameof(True), typeof(object), typeof(If), new PropertyMetadata(default, OnContentPropertyChanged));

    #endregion

    #region False

    public object False
    {
        get => GetValue(FalseProperty);
        set => SetValue(FalseProperty, value);
    }

    public static readonly DependencyProperty FalseProperty =
        DependencyProperty.Register(nameof(False), typeof(object), typeof(If), new PropertyMetadata(default, OnContentPropertyChanged));

    #endregion

    #region Condition

    public bool Condition
    {
        get => (bool)GetValue(ConditionProperty);
        set => SetValue(ConditionProperty, value);
    }

    public static readonly DependencyProperty ConditionProperty =
        DependencyProperty.Register(nameof(Condition), typeof(bool), typeof(If), new PropertyMetadata(default(bool), OnContentPropertyChanged));

    #endregion

    private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not If @if) return;
        @if.Content = @if.Condition ? @if.True : @if.False;
    }
}