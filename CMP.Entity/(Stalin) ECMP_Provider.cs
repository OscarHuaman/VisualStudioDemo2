/*********************************************************
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 11/02/2016
**********************************************************/
using ComputerSystems;
using System;

namespace CMP.Entity
{
    public class ECMP_DocumentFromNote  : CmpNotifyPropertyChanged
    {
        public ECMP_DocumentFromNote()
        {
            Monto = Convert.ToDecimal("0").ToString("N2");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-PE");
        }

        public int Id { get; set; }
        public string IdDocumento { get; set; }
        public string Document { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string Recorded { get; set; }
        public string Exonerated { get; set; }
        public string MontIgv { get; set; }
        public string Igv { get; set; }
        public string SymbolMoney { get; set; }
        public string TypeMoney { get; set; }
        public bool IncluyeIgv { get; set; }
        public string ChangeType { get; set; }
        public string IdMoney { get; set; }
        public string State { get; set; }
        public ECMP_NoteCreditDebit NotaCreditoDebito { get; set; }

        private bool type;
        private string amount;
        private string monto;

        public ControlMonto Documento { get; set; }
        public bool Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
                OnPropertyChanged();
                if (Documento == null)
                {
                    Documento = new ControlMonto();
                    Documento.Grabada = Recorded;
                    Documento.Exonerada = Exonerated;
                    Documento.Monto = Amount;
                    Documento.Igv = Igv;
                }
                if (value == false)
                {
                    if (Documento != null)
                    {
                        Recorded = Documento.Grabada;
                        Exonerated = Documento.Exonerada;
                        Amount = Documento.Monto;
                        Igv = Documento.Igv;
                    }
                }
                else
                {
                    if (Convert.ToDecimal(Monto == "" ? "0" : Monto) == 0)
                    {
                        Recorded = Convert.ToDecimal("0").ToString("N2");
                        Exonerated = Convert.ToDecimal("0").ToString("N2");
                        Amount = Convert.ToDecimal("0").ToString("N2");
                        Igv = Convert.ToDecimal("0").ToString("N2");
                    }
                    else
                    {
                        CalculoInterno(Monto == "" ? "0" : Monto);
                    }
                }
                if (NotaCreditoDebito != null)
                    NotaCreditoDebito.CalculatePrices();
            }
        }

        public string Monto
        {
            get
            {
                return monto;
            }

            set
            {
                monto = value;
                OnPropertyChanged();
                if (value != null)
                {
                    CalculoInterno(value);
                    if (NotaCreditoDebito != null)
                        NotaCreditoDebito.CalculatePrices();
                }
            }
        }

        public string Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        private void CalculoInterno(string value)
        {
            try
            {
                value = value.Length == 0 ? "0" : value;
                if (Convert.ToDecimal(Exonerated) == 0)
                {
                    if (IncluyeIgv == true)
                    {
                        Recorded = Convert.ToDecimal(Convert.ToDecimal(value) / (1 + Convert.ToDecimal(MontIgv))).ToString("N2");
                        Amount = Convert.ToDecimal(value).ToString("N2");
                        Igv = Convert.ToDecimal(Convert.ToDecimal(value) - Convert.ToDecimal(Recorded)).ToString("N2");
                    }
                    else
                    {
                        Recorded = Convert.ToDecimal(value).ToString("N2");
                        Igv = IdDocumento == "HNR" ? Convert.ToDecimal(Convert.ToDecimal(value) * (Convert.ToDecimal(MontIgv))).ToString("N2") : Convert.ToDecimal(Convert.ToDecimal(value) * (Convert.ToDecimal(MontIgv))).ToString("N2");
                        Amount = IdDocumento == "HNR" ? Convert.ToDecimal(Convert.ToDecimal(Recorded) - Convert.ToDecimal(Igv)).ToString("N2") : Convert.ToDecimal(Convert.ToDecimal(Recorded) + Convert.ToDecimal(Igv)).ToString("N2");
                    }
                }
                else
                {
                    Exonerated = Convert.ToDecimal(value).ToString("N2");
                    Recorded = Convert.ToDecimal("0").ToString("N2");
                    Igv = Convert.ToDecimal("0").ToString("N2");
                    Amount = value;
                }
                if (Documento != null)
                {
                    if (Convert.ToDecimal(value) >= Convert.ToDecimal(Documento.Monto))
                    {
                        Recorded = Convert.ToDecimal("0").ToString("N2");
                        Exonerated = Convert.ToDecimal("0").ToString("N2");
                        Amount = Convert.ToDecimal("0").ToString("N2");
                        Igv = Convert.ToDecimal("0").ToString("N2");
                        Monto = "";
                        throw new Exception("El monto ingresado no puede superar el importe total del registro");
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }

    public class ControlMonto
    {
        public string Grabada { get; set; }
        public string Exonerada { get; set; }
        public string Monto { get; set; }
        public string Igv { get; set; }
    }

    public class ECMP_Detail
    {
        public int IdCompra { get; set; }
        public decimal Import { get; set; }
    }

    public class ECMP_BranchOffice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCompany { get; set; }
        public string Reason { get; set; }
        public string Ruc { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
