using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace menDoc.Common.Converters
{
    public class IgnoreNewItemPlaceholderConverter : IValueConverter
    {
        private const string newItemPlaceholderName = "{NewItemPlaceholder}";
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value.ToString() == newItemPlaceholderName)
                value = DependencyProperty.UnsetValue;
            return value;
        }
    }
}
