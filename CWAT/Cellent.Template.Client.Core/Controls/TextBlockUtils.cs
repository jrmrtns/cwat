using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Cellent.Template.Client.Core.Controls
{
    /// <summary>
    /// Textblock utils used in the CellentListview to show/hide the tooltip
    /// when the text is trimmed
    /// </summary>
    public class TextBlockUtils 
    {
        /// <summary>
        /// Gets the value of the AutoTooltipProperty dependency property
        /// </summary>
        public static bool GetAutoTooltip(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoTooltipProperty);
        }

        /// <summary>
        /// Sets the value of the AutoTooltipProperty dependency property
        /// </summary>
        public static void SetAutoTooltip(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoTooltipProperty, value);
        }

        /// <summary>
        /// Identified the attached AutoTooltip property. When true, this will set the TextBlock TextTrimming
        /// property to WordEllipsis, and display a tooltip with the full text whenever the text is trimmed.
        /// </summary>
        public static readonly DependencyProperty AutoTooltipProperty = DependencyProperty.RegisterAttached("AutoTooltip",
                typeof(bool), typeof(TextBlockUtils), new PropertyMetadata(false, OnAutoTooltipPropertyChanged));

        private static void OnAutoTooltipPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBlock textBlock = d as TextBlock;
            if (textBlock == null)
                return;

            if (e.NewValue.Equals(true))
            {
                textBlock.TextTrimming = TextTrimming.WordEllipsis;
                ComputeAutoTooltip(textBlock);
                textBlock.SizeChanged += TextBlock_SizeChanged;
            }
            else
            {
                textBlock.SizeChanged -= TextBlock_SizeChanged;
            }
        }

        private static void TextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ComputeAutoTooltip(sender as TextBlock);
        }

        /// <summary>
        /// Assigns the ToolTip for the given TextBlock based on whether the text is trimmed
        /// </summary>
        private static void ComputeAutoTooltip(TextBlock textBlock)
        {

            textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            var width = textBlock.DesiredSize.Width;

            var typeface = new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch);
            var formattedText = new FormattedText(textBlock.Text, CultureInfo.CurrentCulture, textBlock.FlowDirection, typeface, textBlock.FontSize, textBlock.Foreground);

            if (formattedText.Width <= textBlock.ActualWidth)
                ToolTipService.SetToolTip(textBlock, null); 
            else
                ToolTipService.SetToolTip(textBlock, textBlock.Text);
        }
    }
}
