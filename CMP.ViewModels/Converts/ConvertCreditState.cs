using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CMP.ViewModels.Converts
{
    public class ConvertCreditState : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var State = (string)value;
            if (State == "C")
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var State = (bool)value;
            if (State == true)
                return "C";
            return "D";
        }
    }
}
