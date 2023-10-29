using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Tools
{
    public class TimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hour = (int)value;
            if (hour > 12)
            {
                return $"{hour - 12} pm";
            }
            else if (hour == 0)
            {
                return "12 am";
            }
            else if (hour == 12)
            {
                return "12 pm";
            }
            else
            {
                return $"{hour} am";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
