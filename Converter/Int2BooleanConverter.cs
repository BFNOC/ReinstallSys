using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ReinstallSys.Converter
{
    [ValueConversion(typeof(int), typeof(bool))]
    public class Int2BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int c = System.Convert.ToInt32(parameter);
            if (value == null)
                throw new ArgumentNullException("value can not be null");
            int index = System.Convert.ToInt32(value);
            if (index == -1)
                return false;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
