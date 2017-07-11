/*********************************************************
'* ENTIDAD PARA LA TABLA ORDEN COMPRA
'* CREADO POR	 : COMPUTER SYSTEMS SOLUTION
'*				   ABEL QUISPE ORELLANA
'* FCH. CREACIÓN : 19/11/2015
**********************************************************/
namespace CMP.Entity
{
    using ALM.Entity;
    using ComputerSystems;
    using MNF.Entity;
    using SGC.Empresarial.Entity;
    using System;

    [Serializable]
    public class ECMP_Compra : CmpNotifyPropertyChanged
    {
        public string Opcion { get; set; }
        public int IdCompra { get; set; }
        public EMNF_ClienteProveedor ObjEMNF_ClienteProveedor { get; set; }
        public ESGC_FormaPago ObjESGC_FormaPago { get; set; }
        public EMNF_OperacionMovimiento ObjEMNF_OperacionMovimiento { get; set; }
        public EMNF_MotivoMovimiento ObjEMNF_MotivoMovimiento { get; set; }
        public EMNF_SubDiario ObjEMNF_SubDiario { get; set; }
        public ESGC_Estado ObjESGC_Estado { get; set; }
        public ESGC_Moneda ObjESGC_Moneda { get; set; }
        public EMNF_MedioPago ObjEMNF_MedioPago { get; set; }
        public EMNF_TipoDestino ObjEMNF_TipoDestino { get; set; }
        public string CodDocumento { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public string Periodo { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaContable { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TipoCambio { get; set; }
        public decimal Gravada { get; set; }
        public decimal Exonerada { get; set; }
        public decimal IGV { get; set; }
        public decimal ImporteIGV { get; set; }
        public bool IncluyeIGV { get; set; }
        public bool _AfectoDetraccion;
        public bool AfectoDetraccion 
        {
            get 
            {
                return _AfectoDetraccion;
            }
            set 
            {
                _AfectoDetraccion = value;
            }
        }
        public bool AfectoPercepcion { get; set; }
        public decimal Detraccion { get; set; }
        public decimal Percepcion { get; set; }
        public bool Retencion { get; set; }
        public string Glosa { get; set; }
        public string GuiaRemision { get; set; }
        public string CodDocumentoRef { get; set; }
        public string CadenaXML { get; set; }
        public string DocumentoRefXML { get; set; }
        public string Observacion { get; set; }
        private decimal _Total;
        public decimal Total 
        {
            get
            {
                return _Total;
            }
            set
            {
                _Total = value;
                OnPropertyChanged("Total");
            }
        }
        public string Descripcion { get; set; }
        public string SerieRef { get; set; }
        public string NumeroRef { get; set; }
        public string BProvicionadoText { get; set; }
        public ECMP_Compra() { }
        public bool AfectaAlmacen { get; set; }
        public bool Distribucion { get; set; }
        public string DescDocumento { get; set; }
        public string CodEstado { get; set; }
        //public string TipoDestino { get; set; }
        public bool Anticipo { get; set; }
        private string _SerieNumero = string.Empty;
        public string SerieNumero
        {
            get
            {
                if (_SerieNumero.Trim().Length == 0)
                    _SerieNumero = Serie + " - " + Numero;
                return _SerieNumero;
            }
            set
            {
                _SerieNumero = value;
            }
        }
        public string CompraAnticipoXML { get; set; }

        public bool _PagoDetraccion;
        public bool PagoDetraccion
        {
            get
            {
                return _PagoDetraccion;
            }
            set
            {
                _PagoDetraccion = value;
            }
        }

        public decimal SaldoCompra { get; set; }
        public decimal DetraccionCompra { get; set; }

        private bool _IncluyeDetraccionCompra;
        public bool IncluyeDetraccionCompra
        {
            get
            {
                return _IncluyeDetraccionCompra;
            }
            set
            {
                _IncluyeDetraccionCompra = value;
            }
        }

        public bool CajaBanco { get; set; }
        public bool Planilla { get; set; }

        public ECMP_Compra(object DataGridSelectedItem, TipoConstructor MyTipoConstructor)
        {
            if (MyTipoConstructor == TipoConstructor.Insert || DataGridSelectedItem == null)
            {
                this.Opcion = "I";
                this.CodDocumentoRef = "CMP";
                this.Fecha = DateTime.Now;
                this.FechaContable = DateTime.Now;
                this.ObjESGC_Estado = new ESGC_Estado();
                this.TipoCambio = 1;
                this.ObjESGC_Estado = new ESGC_Estado() { CodEstado = "PECMP" };
                this.ObjEMNF_TipoDestino = new EMNF_TipoDestino();
                this.CodDocumento = string.Empty;
                this.Serie = string.Empty;
                this.Numero = string.Empty;
                this.AfectaAlmacen = true;
                this.Anticipo = false;
                this.CajaBanco = false;
                this.Planilla = false;
            }
            else
            {
                if (DataGridSelectedItem is ECMP_Compra)
                {
                    var ObjECMP_Compra = (ECMP_Compra)DataGridSelectedItem;
                    this.Opcion = (MyTipoConstructor == TipoConstructor.Update) ? "U" : "D";
                    this.IdCompra = ObjECMP_Compra.IdCompra;
                    this.ObjEMNF_ClienteProveedor = ObjECMP_Compra.ObjEMNF_ClienteProveedor;
                    this.ObjESGC_FormaPago = ObjECMP_Compra.ObjESGC_FormaPago;
                    this.ObjEMNF_OperacionMovimiento = ObjECMP_Compra.ObjEMNF_OperacionMovimiento;
                    this.ObjEMNF_MotivoMovimiento = ObjECMP_Compra.ObjEMNF_MotivoMovimiento;
                    this.ObjEMNF_SubDiario = ObjECMP_Compra.ObjEMNF_SubDiario;
                    this.ObjESGC_Estado = ObjECMP_Compra.ObjESGC_Estado;
                    this.ObjESGC_Moneda = ObjECMP_Compra.ObjESGC_Moneda;
                    this.ObjEMNF_MedioPago = ObjECMP_Compra.ObjEMNF_MedioPago;
                    this.ObjEMNF_TipoDestino = ObjECMP_Compra.ObjEMNF_TipoDestino;
                    this.CodDocumento = ObjECMP_Compra.CodDocumento;
                    this.Serie = ObjECMP_Compra.Serie;
                    this.Numero = ObjECMP_Compra.Numero;
                    this.Periodo = ObjECMP_Compra.Periodo;
                    this.Fecha = ObjECMP_Compra.Fecha;
                    this.FechaContable = ObjECMP_Compra.FechaContable;
                    this.FechaVencimiento = ObjECMP_Compra.FechaVencimiento;
                    this.TipoCambio = ObjECMP_Compra.TipoCambio;
                    this.Gravada = ObjECMP_Compra.Gravada;
                    this.Exonerada = ObjECMP_Compra.Exonerada;
                    this.IGV = ObjECMP_Compra.IGV;
                    this.ImporteIGV = ObjECMP_Compra.ImporteIGV;
                    this.IncluyeIGV = ObjECMP_Compra.IncluyeIGV;
                    this.AfectoDetraccion = ObjECMP_Compra.AfectoDetraccion;
                    this.AfectoPercepcion = ObjECMP_Compra.AfectoPercepcion;
                    this.Detraccion = ObjECMP_Compra.Detraccion;
                    this.Percepcion = ObjECMP_Compra.Percepcion;
                    this.Glosa = ObjECMP_Compra.Glosa;
                    this.GuiaRemision = ObjECMP_Compra.GuiaRemision;
                    this.AfectaAlmacen = ObjECMP_Compra.AfectaAlmacen;
                    this.CodDocumentoRef = ObjECMP_Compra.CodDocumentoRef;
                    this.SerieRef = ObjECMP_Compra.SerieRef;
                    this.NumeroRef = ObjECMP_Compra.NumeroRef;
                    this.Anticipo = ObjECMP_Compra.Anticipo;
					this.Retencion = ObjECMP_Compra.Retencion;
                    this.Distribucion = ObjECMP_Compra.Distribucion;
                    this.CajaBanco = ObjECMP_Compra.CajaBanco;
                    this.Planilla = ObjECMP_Compra.Planilla;
                }
            }
        }
    }
}
