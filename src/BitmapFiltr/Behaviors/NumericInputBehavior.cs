using System.Globalization;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Interactivity;
using System.Text.RegularExpressions;

namespace BitmapFiltr.Behaviors;

public class NumericInputBehavior : Behavior<TextBox>
{
    protected override void OnAttached()
    {
        AssociatedObject.PreviewTextInput += OnPreviewTextInput;
        DataObject.AddPastingHandler(AssociatedObject, OnPaste);
        base.OnAttached();
    }

    private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        TextBox textBox = sender as TextBox;
        string newText = textBox.Text.Insert(textBox.SelectionStart, e.Text);

        e.Handled = !IsValidInput(newText);
    }

    private void OnPaste(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetData(typeof(string)) is string pastedText && !IsValidInput(pastedText))
        {
            e.CancelCommand();
        }
    }

    private bool IsValidInput(string input)
    {
        string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        string pattern = $@"^-?\d*({Regex.Escape(decimalSeparator)}\d*)?$";

        return Regex.IsMatch(input, pattern);
    }
}