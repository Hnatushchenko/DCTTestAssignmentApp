using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DCTTestAssignment.Data.CustomConverters;

public class DictionaryDoubleValuesBindingConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Dictionary<string, double> dictionary)
        {
            if (parameter is string key)
            {
                return dictionary.GetValueOrDefault(key);
            }
        }
        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
