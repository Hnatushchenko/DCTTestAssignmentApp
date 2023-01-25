using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DCTTestAssignment.Data.CustomConverters;

public class DictionaryValuesBindingConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Dictionary<string, decimal?> dictionary)
        {
            if (parameter is string key)
            {
                return dictionary[key];
            }
        }
        return "not converted";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
