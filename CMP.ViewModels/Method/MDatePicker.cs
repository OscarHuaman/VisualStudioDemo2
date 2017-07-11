using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CMP.ViewModels.Method
{
    public class MDatePicker
    {
        public static void DateStartToDateEnd(string Periodo, out DateTime? DatetimeStart, out DateTime DatetimeEnd, out DateTime SelectedDate, bool DateStart = false)
        {
            int intAnio = Convert.ToInt32(Periodo.Substring(0, 4));
            int intMes = Convert.ToInt32(Periodo.Substring(4, 2));

            if (intMes == DateTime.Now.Month)
                SelectedDate = DateTime.Now.Date;
            else
                SelectedDate = new DateTime(intAnio, intMes, 1);

            if (DateStart)
                DatetimeStart = new DateTime(intAnio, intMes, 1);
            else
                DatetimeStart = null;

            if (intMes == DateTime.Now.Month)
                DatetimeEnd = DateTime.Now.Date;
            else
                DatetimeEnd = new DateTime(intAnio, intMes + 1, 1).AddDays(-1);
        }

        public static void DateStartToDateEndOP(DatePicker _DateTimePicker, string Periodo, bool DateStart = false)
        {
            int intAnio = Convert.ToInt32(Periodo.Substring(0, 4));
            int intMes = Convert.ToInt32(Periodo.Substring(4, 2));

            if (intMes == DateTime.Now.Month)
                _DateTimePicker.SelectedDate = DateTime.Now.Date;
            else
                _DateTimePicker.SelectedDate = new DateTime(intAnio, intMes, 1);

            if (DateStart)
                _DateTimePicker.DisplayDateStart = new DateTime(intAnio, intMes, 1);

            if (intMes == DateTime.Now.Month)
                _DateTimePicker.DisplayDateEnd = DateTime.Now.Date;
            else
                _DateTimePicker.DisplayDateEnd = new DateTime(intAnio, intMes + 1, 1).AddDays(-1);
        }
    }
}
