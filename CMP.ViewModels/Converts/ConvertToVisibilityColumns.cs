/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				  Abel Quispe Orellana
'* FCH. CREACIÓN : 31/10/2016
**********************************************************/
using System;
using System.Windows;
using System.Windows.Data;
namespace CMP.ViewModels.Converts
{
    class ConvertToVisibilityColumns : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (bool)value;
            if (val == true)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
