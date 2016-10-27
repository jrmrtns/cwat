using System;
using System.Globalization;
using System.Windows.Data;

namespace Cellent.Template.Client.Core.ValueConverter
{
    /// <summary>
    /// DebugConverter
    /// </summary>
    public class DebugConverter : IValueConverter
    {
        /// <summary>
        /// Gibt den Wert zurück
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        /// <summary>
        /// Convert Back
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
