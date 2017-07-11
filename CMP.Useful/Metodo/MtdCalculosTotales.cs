/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 11/02/2016
**********************************************************
'* MODIFICADO POR : COMPUTER SYSTEMS SOLUTION
'*				    OSCAR HUAMAN CABRERA
'* FCH. MODIFICADO : 26/09/2016
**********************************************************/
namespace CMP.Useful.Metodo
{
    using System;
    using System.Globalization;
    using System.Windows.Controls;

    public class MtdCalculosTotales
    {
        char Separator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        string TempValueGravada = string.Empty;
        string numEnteroGravada;
        string numDecimalGravada;

        string TempValueTotalIGV = string.Empty;
        string numEnteroTotalIGV;
        string numDecimalTotalIGV;

        bool IsGravada = false;

        public void Calcular(TextBox Gravada, TextBox TotalIGV, bool IsGravada, Action MyActionNotific)
        {
            try
            {
                TempValueGravada = string.Empty;
                TempValueTotalIGV = string.Empty;

                TempValueGravada = Gravada.Text;
                TempValueTotalIGV = TotalIGV.Text;

                if (Gravada.Text.Trim().IndexOf(Separator) < 0 || TotalIGV.Text.Trim().IndexOf(Separator) < 0) return;


                int indexOfGravada = 0;
                int indexOfTotalIGV = 0;

                if (Separator == Convert.ToChar(","))
                {
                    indexOfGravada = TempValueGravada.IndexOf(",") + 1;
                    indexOfGravada = TempValueTotalIGV.IndexOf(",") + 1;
                }
                else
                {
                    indexOfGravada = TempValueGravada.IndexOf(".") + 1;
                    indexOfTotalIGV = TempValueTotalIGV.IndexOf(".") + 1;
                }

                numEnteroGravada = TempValueGravada.Substring(0, indexOfGravada -1);
                numDecimalGravada = TempValueGravada.Substring(indexOfGravada, TempValueGravada.Length - indexOfGravada);

                numEnteroTotalIGV = TempValueTotalIGV.Substring(0, indexOfTotalIGV -1);
                numDecimalTotalIGV = TempValueTotalIGV.Substring(indexOfTotalIGV, TempValueTotalIGV.Length - indexOfTotalIGV);

                this.IsGravada = IsGravada;

                if (IsGravada)
                {
                    Gravada.Text = numDecimalGravada.ToString();
                    Gravada.SelectAll();
                }
                else
                {
                    TotalIGV.Text = numDecimalTotalIGV.ToString();
                    TotalIGV.SelectAll();
                }

                numDecimalGravada = "0" + Separator + numDecimalGravada;
                numDecimalTotalIGV = "0" + Separator + numDecimalTotalIGV;

                if (MyActionNotific != null)
                    MyActionNotific.Invoke();

            }
            catch (Exception)
            {
                Gravada.Text = (IsGravada) ? numEnteroGravada + "." + TempValueGravada : TempValueGravada;
                TotalIGV.Text = (!IsGravada) ? numEnteroTotalIGV + "." + TempValueTotalIGV : TempValueTotalIGV;
                throw;
            }
        }

        public void NewValue(TextBox Gravada, TextBox TotalIGV, decimal TotalNeto, SumarOrRestarGravadaTotalIGV MySumarOrRestarGravadaTotalIGV = SumarOrRestarGravadaTotalIGV.SUMAR)
        {
            try
            {
                decimal SumaGravadaTotalIGV = 0;
                decimal NewValueGravada = 0;
                decimal NewValueTotalIGV = 0;
                bool blHayCambio = true;
                
                if (IsGravada)
                {
                    decimal TempNewDecimalGravada = Convert.ToDecimal("0" + Separator + Gravada.Text);

                    if (TempNewDecimalGravada ==  Convert.ToDecimal(numDecimalGravada))
                        blHayCambio = false;

                    NewValueTotalIGV = Convert.ToDecimal(numEnteroTotalIGV) + Convert.ToDecimal(numDecimalTotalIGV);
                    NewValueGravada = Convert.ToDecimal(numEnteroGravada) + Convert.ToDecimal(TempNewDecimalGravada);
                }
                else
                {
                    decimal TempNewTotalIGV = Convert.ToDecimal("0" + Separator + TotalIGV.Text);

                    if (TempNewTotalIGV == Convert.ToDecimal(numDecimalTotalIGV))
                        blHayCambio = false;

                    NewValueGravada = Convert.ToDecimal(numEnteroGravada) + Convert.ToDecimal(numDecimalGravada);
                    NewValueTotalIGV = Convert.ToDecimal(numEnteroTotalIGV) + Convert.ToDecimal(TempNewTotalIGV);
                }

                if (blHayCambio)
                    SumaGravadaTotalIGV = (MySumarOrRestarGravadaTotalIGV == SumarOrRestarGravadaTotalIGV.SUMAR) ? NewValueGravada + NewValueTotalIGV : NewValueGravada - NewValueTotalIGV;

                    Gravada.Text = (NewValueGravada).ToString("###,###,##0.#0");
                    TotalIGV.Text = (NewValueTotalIGV).ToString("###,###,##0.#0");
            }
            catch (Exception ex)
            {
                if (TempValueGravada.IndexOf(Separator) < 0)
                    Gravada.Text = (IsGravada) ? numEnteroGravada + "." + TempValueGravada : TempValueGravada;
                if (TempValueTotalIGV.IndexOf(Separator) < 0)
                    TotalIGV.Text = (!IsGravada) ? numEnteroTotalIGV + "." + TempValueTotalIGV : TempValueTotalIGV;

                if (ex.Message.IndexOf("2628") == 0)
                    throw new Exception(ex.Message.Substring(4, ex.Message.Length - 4));
            }
        }
    }

    public enum SumarOrRestarGravadaTotalIGV 
    {
        SUMAR,
        RESTAR
    }
}
