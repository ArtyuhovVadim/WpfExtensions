using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace WpfExtensions.Behaviors;

public class MultilineTextBoxReturnBehaviour : Behavior<TextBox>
{
    protected override void OnAttached() => AssociatedObject.PreviewKeyDown += OnKeyDown;

    protected override void OnDetaching() => AssociatedObject.PreviewKeyDown -= OnKeyDown;

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.Modifiers != ModifierKeys.Shift || e.Key != Key.Enter) return;

        var caretIndex = AssociatedObject.CaretIndex;
        AssociatedObject.Text = AssociatedObject.Text.Insert(caretIndex, "\n");
        AssociatedObject.CaretIndex = caretIndex + 1;
        e.Handled = true;
    }
}