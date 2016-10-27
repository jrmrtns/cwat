using System.Windows;
using System.Windows.Controls;

namespace Cellent.Template.Client.Core.Extensions
{
    /// <summary>
    /// Focus extenison
    /// </summary>
    public static class FocusExtension
    {
        /// <summary>
        /// IsFoucused Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
             "IsFocused", typeof(bool), typeof(FocusExtension),
             new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        /// <summary>
        /// Gets Is Focused
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        /// <summary>
        /// Sets Is Focused
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        private static void OnIsFocusedPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if ((bool)e.NewValue)
            {
                uie.Focus(); // Don't care about false values.

                if (uie is TextBox)
                    ((TextBox)uie).SelectAll();
            }
        }
    }
}