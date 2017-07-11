using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CMP.Presentation.Method
{
    public class MDatePicker
    {
        public static void DateStartToDateEnd(DatePicker _DateTimePicker, string Periodo, bool DateStart = false)
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
            {
                var vrFecha = new DateTime(((intAnio != DateTime.Now.Year) ? DateTime.Now.Year : intAnio), ((intMes != 12) ? (intMes + 1) : 1), 1);
                _DateTimePicker.DisplayDateEnd = vrFecha.AddDays(-1);
            }
        }
    }
}
